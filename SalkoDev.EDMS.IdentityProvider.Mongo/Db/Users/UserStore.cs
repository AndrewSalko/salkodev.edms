using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SalkoDev.BaseClasses;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users
{
	public class UserStore : DisposableBase, IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>, IUserStoreEx
	{
		//https://www.eximiaco.tech/en/2019/07/27/writing-an-asp-net-core-identity-storage-provider-from-scratch-with-ravendb/

		readonly IMongoCollection<User> _Users;

		public UserStore(IDatabase db)
		{
			var coll = new UsersCollection(db, UsersCollection.COLLECTION_USERS, true);
			_Users = coll.Users;
		}

		public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
		{
			await _Users.InsertOneAsync(user);

			return IdentityResult.Success;
		}

		public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			//если есть идентификатор, брать его (корректно ли так проверять?)
			if (user.Id.CreationTime.Ticks != 0)
			{
				await _Users.DeleteOneAsync(usr => usr.Id == user.Id);
			}
			else
			{
				string emailForSearch = user.Email.ToLower();

				await _Users.DeleteOneAsync(usr => usr.Email.ToLower() == emailForSearch);
			}

			return IdentityResult.Success;
		}

		public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(userId))
				throw new ArgumentNullException(nameof(userId));

			MongoDB.Bson.ObjectId id = new MongoDB.Bson.ObjectId(userId);

			var resultUser = await (from user in _Users.AsQueryable() where user.Id==id select user).FirstOrDefaultAsync(cancellationToken);
			return resultUser;
		}

		public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
		{
			string result = user.Id.ToString();
			return await Task.FromResult(result);
		}

		public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
		{
			return await Task.FromResult(user.Name);
		}

		public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			//найти по email
			var email = normalizedEmail.ToLower();

			var resultUser = await (from user in _Users.AsQueryable() where user.Email.ToLower() == email select user).FirstOrDefaultAsync(cancellationToken);
			return resultUser;
		}

		public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
		{
			return await Task.FromResult(user.Email);
		}

		public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
		{
			//сохранять нормализованный Email не будем
			return Task.CompletedTask;
		}


		public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			//Name у нас Email
			var user = await FindByEmailAsync(normalizedUserName, cancellationToken);

			return user;
		}

		public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
		{
			//У нас имя равно email, так что сохранять нормализ.имя просто не нужно
			return Task.CompletedTask;
		}

		public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
		{
			user.PasswordHash = passwordHash;

			return Task.CompletedTask;
		}

		public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
		{
			//найти юзера...сверить, какие поля разные

			var id = user.Id;

			var resultUser = await (from userDB in _Users.AsQueryable() where userDB.Id == id select userDB).FirstOrDefaultAsync(cancellationToken);
			if (resultUser == null)
				throw new ArgumentException($"User Id {id}");

			var filter = Builders<User>.Filter.Eq(x => x.Id, id);

			List<UpdateDefinition<User>> updateList = new List<UpdateDefinition<User>>();

			if (resultUser.PasswordHash != user.PasswordHash)
			{
				var upd = Builders<User>.Update.Set(x => x.PasswordHash, user.PasswordHash);
				updateList.Add(upd);
			}

			if (resultUser.Email != user.Email)
			{
				var upd = Builders<User>.Update.Set(x => x.Email, user.Email);
				updateList.Add(upd);
			}

			if (updateList.Count > 0)
			{
				var complexUpd = Builders<User>.Update.Combine(updateList);

				var updateResult = _Users.UpdateOne(filter, complexUpd);
				//TODO@: сверить результат..?
			}

			return IdentityResult.Success;
		}

		public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
		{
			//одно из применений - при логине, сюда придет уже "найденный" в базе юзер и надо лишь вернуть хеш пароля
			return await Task.FromResult(user.PasswordHash);
		}

		public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
		{
			//в данном случае все достаточно просто - объект юзер уже наполнен свойствами
			return Task.FromResult(user.EmailConfirmed);
		}

		public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
		{
			var id = user.Id;

			var resultUser = await (from userDB in _Users.AsQueryable() where userDB.Id == id select userDB).FirstOrDefaultAsync(cancellationToken);
			if (resultUser == null)
				throw new ArgumentException($"User Id {id}");

			if (resultUser.EmailConfirmed == confirmed)
			{
				return; //нечего устанавливать, тек.значение совпадает
			}

			var filter = Builders<User>.Filter.Eq(x => x.Id, id);

			List<UpdateDefinition<User>> updateList = new List<UpdateDefinition<User>>();

			var upd = Builders<User>.Update.Set(x => x.EmailConfirmed, confirmed);
			updateList.Add(upd);

			if (updateList.Count > 0)
			{
				var complexUpd = Builders<User>.Update.Combine(updateList);
				var updateResult = _Users.UpdateOne(filter, complexUpd);
			}
		}

		#region Not implemented methods

		public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();

			//https://stackoverflow.com/questions/45340273/explanation-of-getnormalizedusernameasync-and-setnormalizedusernameasync-functio
			//GetNormalizedUserNameAsync isn't used anywhere in the Identity framework.
			//return await Task.FromResult(user.Name);
		}

		public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		#endregion

		public async Task SetUserOrganizationAsync(IUser user, string orgUID)
		{
			if (user.OrganizationUID == orgUID)
				return;

			var filter = Builders<User>.Filter.Eq(x => x.Id, user.Id);

			List<UpdateDefinition<User>> updateList = new List<UpdateDefinition<User>>();

			var upd = Builders<User>.Update.Set(x => x.OrganizationUID, orgUID);
			updateList.Add(upd);

			if (updateList.Count > 0)
			{
				var complexUpd = Builders<User>.Update.Combine(updateList);
				var updateResult = await _Users.UpdateOneAsync(filter, complexUpd);
			}
		}


		protected override void _Dispose(bool disposing)
		{
			if (disposing)
			{
			}
		}

	}
}

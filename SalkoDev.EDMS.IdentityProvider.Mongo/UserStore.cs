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

namespace SalkoDev.EDMS.IdentityProvider.Mongo
{
	public class UserStore : DisposableBase, IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>
	{
		//https://www.eximiaco.tech/en/2019/07/27/writing-an-asp-net-core-identity-storage-provider-from-scratch-with-ravendb/

		IMongoCollection<User> _Users;

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

			//TODO@: будет ли это применяться к уже сущ.юзерам

			////найти юзера по email, установить хеш пароля
			//if (user == null)
			//	throw new ArgumentNullException(nameof(user));

			//var email = user.Email;
			//if (string.IsNullOrEmpty(email))
			//	throw new ArgumentNullException(nameof(user), "User.Email empty");

			//var emailNorm = email.ToLower();
			//var resultUser = await(from usr in _Users.AsQueryable() where usr.Email.ToLower() == emailNorm select usr).FirstOrDefaultAsync(cancellationToken);

			//if (resultUser == null)
			//	throw new ArgumentNullException(nameof(user), $"User with email {emailNorm} not found");

			////установить хеш пароля
			//var filter = Builders<User>.Filter.Eq(x => x.Id, user.Id);
			//var update = Builders<User>.Update.Set(x => x.PasswordHash, passwordHash);

			//var updateResult = _Users.UpdateOne(filter, update);
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

		public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
		{
			//одно из применений - при логине, сюда придет уже "найденный" в базе юзер и надо лишь вернуть хеш пароля
			return await Task.FromResult(user.PasswordHash);
		}


		public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}


		public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		#endregion


		protected override void _Dispose(bool disposing)
		{
			if (disposing)
			{
			}
		}

	}
}

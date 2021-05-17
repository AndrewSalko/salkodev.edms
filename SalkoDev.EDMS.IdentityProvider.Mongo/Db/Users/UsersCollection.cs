using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using SalkoDev.EDMS.Mongo.Db;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users
{
	/// <summary>
	/// Коллекция пользователей (уровень Mongo DB)
	/// </summary>
	public class UsersCollection
	{
		/// <summary>
		/// Коллекция пользователей
		/// </summary>
		public const string COLLECTION_USERS = "Users";

		public UsersCollection(IDatabase database, string collectionName, bool createIndexes)
		{
			if (database == null)
				throw new ArgumentNullException(nameof(database));

			var db = database.DB;

			if (string.IsNullOrEmpty(collectionName))
				collectionName = COLLECTION_USERS;

			Users = db.GetCollection<User>(collectionName);

			if (createIndexes)
			{
				//применим индекс (имя юзера)
				var keysEmail = Builders<User>.IndexKeys.Ascending(x => x.Email);
				var optsEmail = new CreateIndexOptions() { Unique = true };
				var modelEmail = new CreateIndexModel<User>(keysEmail, optsEmail);

				CreateIndexModel<User>[] indexModels = { modelEmail };
				Users.Indexes.CreateMany(indexModels);
			}
		}

		public IMongoCollection<User> Users
		{
			get;
			private set;
		}

	}
}

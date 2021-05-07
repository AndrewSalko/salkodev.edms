using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Organizations
{
	public class OrganizationsCollection
	{
		/// <summary>
		/// Коллекция пользователей
		/// </summary>
		public const string COLLECTION_ORGANIZATIONS = "Organizations";

		public OrganizationsCollection(IDatabase database, string collectionName, bool createIndexes)
		{
			if (database == null)
				throw new ArgumentNullException(nameof(database));

			var db = database.DB;

			if (string.IsNullOrEmpty(collectionName))
				collectionName = COLLECTION_ORGANIZATIONS;

			Organizations = db.GetCollection<Organization>(collectionName);

			if (createIndexes)
			{
				//применим индекс
				var keysName = Builders<Organization>.IndexKeys.Ascending(x => x.Name);
				CreateIndexOptions optsName = null; //new CreateIndexOptions() { Unique = true  };  //нельзя..вероятно?
				var modelName = new CreateIndexModel<Organization>(keysName, optsName);

				var keysFullName = Builders<Organization>.IndexKeys.Ascending(x => x.FullName);
				var modelFullName = new CreateIndexModel<Organization>(keysFullName, null);

				CreateIndexModel<Organization>[] indexModels = { modelName, modelFullName };
				Organizations.Indexes.CreateMany(indexModels);
			}
		}

		public IMongoCollection<Organization> Organizations
		{
			get;
			private set;
		}

	}
}

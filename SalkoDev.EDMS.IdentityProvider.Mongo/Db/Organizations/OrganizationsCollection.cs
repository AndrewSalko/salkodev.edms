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
				//индекс по имени
				CreateIndexModel<Organization> modelName;
				{
					var keysName = Builders<Organization>.IndexKeys.Ascending(x => x.Name);
					CreateIndexOptions optsName = null; //new CreateIndexOptions() { Unique = true  };  //нельзя..вероятно?
					modelName = new CreateIndexModel<Organization>(keysName, optsName);
				}

				//индекс по полному имени
				CreateIndexModel<Organization> modelFullName;
				{
					var keysFullName = Builders<Organization>.IndexKeys.Ascending(x => x.FullName);
					modelFullName = new CreateIndexModel<Organization>(keysFullName, null);
				}

				//индекс по UID
				CreateIndexModel<Organization> modelUID;
				{
					var keysUID = Builders<Organization>.IndexKeys.Ascending(x => x.UID);
					CreateIndexOptions optsUID = new CreateIndexOptions() { Unique = true };  //UID организации должен быть уникальный
					modelUID = new CreateIndexModel<Organization>(keysUID, optsUID);
				}

				CreateIndexModel<Organization>[] indexModels = { modelName, modelFullName, modelUID };
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

using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using SalkoDev.EDMS.Mongo.Db;

namespace SalkoDev.EDMS.Documents.Mongo.Db.Counterparty
{
	public class CounterpartyCollection
	{
		/// <summary>
		/// Коллекция контрагентов
		/// </summary>
		public const string COLLECTION_COUNTERPARTIES = "Counterparties";


		public CounterpartyCollection(IDatabase database, string collectionName, bool createIndexes)
		{
			if (database == null)
				throw new ArgumentNullException(nameof(database));

			var db = database.DB;

			if (string.IsNullOrEmpty(collectionName))
				collectionName = COLLECTION_COUNTERPARTIES;

			Counterparties = db.GetCollection<Counterparty>(collectionName);

			if (createIndexes)
			{
				//индекс по имени
				CreateIndexModel<Counterparty> modelName;
				{
					var keysName = Builders<Counterparty>.IndexKeys.Ascending(x => x.Name);
					CreateIndexOptions optsName = null; //new CreateIndexOptions() { Unique = true  };  //нельзя..вероятно?
					modelName = new CreateIndexModel<Counterparty>(keysName, optsName);
				}

				//индекс по полному имени
				CreateIndexModel<Counterparty> modelFullName;
				{
					var keysFullName = Builders<Counterparty>.IndexKeys.Ascending(x => x.FullName);
					modelFullName = new CreateIndexModel<Counterparty>(keysFullName, null);
				}

				//индекс по UID
				CreateIndexModel<Counterparty> modelUID;
				{
					var keysUID = Builders<Counterparty>.IndexKeys.Ascending(x => x.UID);
					CreateIndexOptions optsUID = new CreateIndexOptions() { Unique = true };  //UID организации должен быть уникальный
					modelUID = new CreateIndexModel<Counterparty>(keysUID, optsUID);
				}

				//индекс по OrgUID
				CreateIndexModel<Counterparty> modelOrgUID;
				{
					var keysOrgUID = Builders<Counterparty>.IndexKeys.Ascending(x => x.OrganizationUID);
					CreateIndexOptions optsOrgUID = null;
					modelOrgUID = new CreateIndexModel<Counterparty>(keysOrgUID, optsOrgUID);
				}



				CreateIndexModel<Counterparty>[] indexModels = { modelName, modelFullName, modelUID, modelOrgUID };
				Counterparties.Indexes.CreateMany(indexModels);
			}
		}

		public IMongoCollection<Counterparty> Counterparties
		{
			get;
			private set;
		}



	}
}

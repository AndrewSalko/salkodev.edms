using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace SalkoDev.Organization.Data.Mongo
{
	public class OrganizationDB
	{
		public const string COLLECTION_ORGANIZATIONS = "Organizations";

		public const string INDEX_FOR_NAME = "Index for 'Name' field";

		public const string INDEX_FOR_DESCRIPTION = "Index for 'Description' field";
		public const string INDEX_FOR_FULLNAME = "Index for 'FullName' field";
		public const string INDEX_FOR_LEGAL_ADDRESS = "Index for 'LegalAddress' field";
		public const string INDEX_FOR_ORG_CODE = "Index for 'OrganizationCode' field";
		public const string INDEX_FOR_PHYSICAL_ADDRESS = "Index for 'PhysicalAddress' field";

		public OrganizationDB()
		{
		}

		/// <summary>
		/// Создает индекс для обычного текстового поля в модели
		/// </summary>
		/// <param name="indexes"></param>
		/// <param name="field"></param>
		/// <param name="indexName"></param>
		void _CreateIndexForField(IMongoIndexManager<Organization> indexes, System.Linq.Expressions.Expression<Func<Organization, object>> field, string indexName)
		{
			var indexKeyDef = Builders<SalkoDev.Organization.Data.Organization>.IndexKeys.Ascending(field);
			CreateIndexOptions ops = new CreateIndexOptions
			{
				Name = indexName
			};

			CreateIndexModel<SalkoDev.Organization.Data.Organization> indModel = new CreateIndexModel<SalkoDev.Organization.Data.Organization>(indexKeyDef, ops);
			indexes.CreateOne(indModel);
		}

		public void Validate(IMongoDatabase database)
		{
			var collection = database.GetCollection<SalkoDev.Organization.Data.Organization>(COLLECTION_ORGANIZATIONS);
			var indexes = collection.Indexes;

			//для скорости нужно создать индексы практически по всем полям..но когда их создавать и как следить за версионностью?
			//вроде как оно "умное" и не должно тормозить если индекс уже создан, но кто его знает?

			_CreateIndexForField(indexes, x => x.Description, INDEX_FOR_DESCRIPTION);
			_CreateIndexForField(indexes, x => x.FullName, INDEX_FOR_FULLNAME);
			_CreateIndexForField(indexes, x => x.LegalAddress, INDEX_FOR_LEGAL_ADDRESS);
			_CreateIndexForField(indexes, x => x.Name, INDEX_FOR_NAME);

			_CreateIndexForField(indexes, x => x.OrganizationCode, INDEX_FOR_ORG_CODE);
			_CreateIndexForField(indexes, x => x.PhysicalAddress, INDEX_FOR_PHYSICAL_ADDRESS);
		}


	}
}

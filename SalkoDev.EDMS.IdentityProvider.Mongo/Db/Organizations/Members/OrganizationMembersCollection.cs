using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using SalkoDev.EDMS.Mongo.Db;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Organizations.Members
{
	class OrganizationMembersCollection
	{
		/// <summary>
		/// Коллекция членов организаций (связывает пользователя и организацию).
		/// Коллекция нужна отдельная, т.к. ключ партиции здесь будет OrganizationUID - чтобы легко находить людей-членов моей организации
		/// </summary>
		public const string COLLECTION_ORGANIZATIONS_MEMBERS = "OrganizationMembers";

		public OrganizationMembersCollection(IDatabase database, string collectionName, bool createIndexes)
		{
			if (database == null)
				throw new ArgumentNullException(nameof(database));

			var db = database.DB;

			if (string.IsNullOrEmpty(collectionName))
				collectionName = COLLECTION_ORGANIZATIONS_MEMBERS;

			OrganizationMembers = db.GetCollection<OrganizationMember>(collectionName);

			if (createIndexes)
			{
				//индекс по UID (не-уникальный)
				CreateIndexModel<OrganizationMember> modelUID;
				{
					var keysUID = Builders<OrganizationMember>.IndexKeys.Ascending(x => x.OrganizationUID);
					CreateIndexOptions optsUID = null;
					modelUID = new CreateIndexModel<OrganizationMember>(keysUID, optsUID);
				}

				//индекс по Email пользователя
				CreateIndexModel<OrganizationMember> modelEmail;
				{
					var keysEmail = Builders<OrganizationMember>.IndexKeys.Ascending(x => x.Email);
					CreateIndexOptions optsEmail = null;
					modelEmail = new CreateIndexModel<OrganizationMember>(keysEmail, optsEmail);
				}

				//индекс по имени пользователя (отображаемое имя)
				CreateIndexModel<OrganizationMember> modelName;
				{
					var keysName = Builders<OrganizationMember>.IndexKeys.Ascending(x => x.Name);
					CreateIndexOptions optsName = null;
					modelName = new CreateIndexModel<OrganizationMember>(keysName, optsName);
				}

				CreateIndexModel<OrganizationMember>[] indexModels = { modelUID, modelEmail, modelName };
				OrganizationMembers.Indexes.CreateMany(indexModels);
			}
		}

		public IMongoCollection<OrganizationMember> OrganizationMembers
		{
			get;
			private set;
		}

	}
}

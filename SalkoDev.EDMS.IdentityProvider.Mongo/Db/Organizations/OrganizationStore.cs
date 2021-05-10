using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Organizations
{
	public class OrganizationStore: IOrganizationStore
	{
		readonly IMongoCollection<Organization> _Orgs;

		public OrganizationStore(IDatabase db)
		{
			var coll = new OrganizationsCollection(db, OrganizationsCollection.COLLECTION_ORGANIZATIONS, true);
			_Orgs = coll.Organizations;
		}

		public async Task<Organization> Create(string name, string fullName, string ownerUserUID)
		{
			var orgUID = Guid.NewGuid().ToString();

			Organization org = new Organization
			{
				Name = name,
				FullName = fullName,
				OwnerUserUID= ownerUserUID,
				UID= orgUID
			};

			await _Orgs.InsertOneAsync(org);

			return org;	//поле Id уже будет заполнено
		}


	}
}

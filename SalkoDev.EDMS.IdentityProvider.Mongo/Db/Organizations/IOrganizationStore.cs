using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Organizations
{
	public interface IOrganizationStore
	{
		Task<Organization> Create(string name, string fullName, string ownerUserUID);
	}
}

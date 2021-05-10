using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users
{
	public interface IUserStoreEx
	{
		Task SetUserOrganizationAsync(IUser user, string orgUID);
	}
}

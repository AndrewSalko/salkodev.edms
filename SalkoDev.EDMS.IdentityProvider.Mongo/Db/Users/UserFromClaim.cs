using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Data;

using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users
{
	public static class UserFromClaim
	{
		public static async Task<IUser> GetUser(UserManager<User> userManager, ClaimsPrincipal claimsPrincipal)
		{
			var userEmail = claimsPrincipal.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

			var user = await userManager.FindByEmailAsync(userEmail);
			return user;
		}
	}
}

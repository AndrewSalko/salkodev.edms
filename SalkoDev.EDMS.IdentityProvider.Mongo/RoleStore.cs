using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using SalkoDev.BaseClasses;

namespace SalkoDev.EDMS.IdentityProvider.Mongo
{
	public class RoleStore : DisposableBase, IRoleStore<Role>
	{
		public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		protected override void _Dispose(bool disposing)
		{
		}

	}
}

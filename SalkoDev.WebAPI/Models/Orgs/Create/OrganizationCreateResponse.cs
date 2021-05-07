using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalkoDev.WebAPI.Models.Orgs.Create
{
	public class OrganizationCreateResponse: BaseResponse
	{
		public OrganizationCreateResponse()
		{
		}

		public OrganizationCreateResponse(string error, bool success)
		{
			Errors = new List<string> { error };
			Success = success;
		}

	}
}

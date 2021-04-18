using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalkoDev.WebAPI.Models.Auth.ChangePassword
{
	public class ChangePasswordResponse : BaseAuthResult
	{
		public ChangePasswordResponse()
		{
		}

		public ChangePasswordResponse(string error, bool success)
		{
			Errors = new List<string> { error };
			Success = success;
		}

	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalkoDev.WebAPI.Models.Auth
{
	public class RegistrationResponse: AuthResult
	{
		public RegistrationResponse()
		{
		}

		public RegistrationResponse(string error, bool success)
		{
			Errors = new List<string> { error };
			Success = success;
		}

	}
}

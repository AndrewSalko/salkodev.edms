using System;
using System.Collections.Generic;

namespace SalkoDev.WebAPI.Models.Auth.Login
{
	public class AuthResult: BaseAuthResult
	{
		public string Token
		{
			get;
			set;
		}

		public DateTime Expires
		{
			get;
			set;
		}

	}
}
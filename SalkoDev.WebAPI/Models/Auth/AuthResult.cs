using System;
using System.Collections.Generic;

namespace SalkoDev.WebAPI.Models.Auth
{
	public class AuthResult
	{
		public string Token
		{
			get;
			set;
		}

		public bool Success
		{
			get;
			set;
		}

		public DateTime Expires
		{
			get;
			set;
		}

		public List<string> Errors
		{
			get;
			set;
		}
	}
}
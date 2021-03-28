using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SalkoDev.WebAPI.Models.Auth
{
	public class UserLoginRequest
	{
		[Required]
		[EmailAddress]
		public string Email
		{
			get;
			set;
		}

		[Required]
		public string Password
		{
			get;
			set;
		}

	}
}

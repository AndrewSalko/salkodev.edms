using System.ComponentModel.DataAnnotations;

namespace SalkoDev.WebAPI.Models.Auth.Login
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

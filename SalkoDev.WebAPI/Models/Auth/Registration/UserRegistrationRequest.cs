using System.ComponentModel.DataAnnotations;

namespace SalkoDev.WebAPI.Models.Auth.Registration
{
	public class UserRegistrationRequest
	{
		/// <summary>
		/// Имя пользователя (отображаемое)
		/// </summary>
		[Required]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Логин (email)
		/// </summary>
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

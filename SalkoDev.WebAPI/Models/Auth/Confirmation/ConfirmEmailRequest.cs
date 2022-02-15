using System.ComponentModel.DataAnnotations;

namespace SalkoDev.WebAPI.Models.Auth.Confirmation
{
	public class ConfirmEmailRequest
	{
		[Required]
		[EmailAddress]
		public string Email
		{
			get;
			set;
		}

		/// <summary>
		/// Токен подтверждения Email
		/// </summary>
		[Required]
		public string ConfirmationToken
		{
			get;
			set;
		}

	}
}

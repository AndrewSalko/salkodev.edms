using System.ComponentModel.DataAnnotations;

namespace SalkoDev.WebAPI.Models.Auth.ChangePassword
{
	public class ChangePasswordRequest
	{
		[Required]
		[EmailAddress]
		public string Email
		{
			get;
			set;
		}

		[Required]
		[DataType(DataType.Password)]
		public string CurrentPassword
		{
			get;
			set;
		}

		[Required]
		[DataType(DataType.Password)]
		public string NewPassword
		{
			get;
			set;
		}

	}
}

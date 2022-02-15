
namespace SalkoDev.WebAPI.Models.Auth.Login
{
	public class AuthResult: BaseResponse
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
using SalkoDev.WebAPI.Models.Auth.Login;

namespace SalkoDev.WebAPI.Models.Auth.ChangePassword
{
	public class ChangePasswordResponse : AuthResult
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

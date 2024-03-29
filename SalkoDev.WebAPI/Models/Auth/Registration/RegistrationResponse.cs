using SalkoDev.WebAPI.Models.Auth.Login;

namespace SalkoDev.WebAPI.Models.Auth.Registration
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

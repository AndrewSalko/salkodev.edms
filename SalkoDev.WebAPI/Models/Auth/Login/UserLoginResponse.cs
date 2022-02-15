
namespace SalkoDev.WebAPI.Models.Auth.Login
{
	public class UserLoginResponse: BaseResponse
	{
		/// <summary>
		/// Статик-хелпер для генерации ответа-ошибки
		/// </summary>
		/// <param name="error"></param>
		/// <returns></returns>
		public static UserLoginResponse Failed(string error)
		{
			return new UserLoginResponse()
			{
				Success = false,
				Errors = new List<string>() { error }
			};
		}

	}
}

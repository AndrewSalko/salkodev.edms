
namespace SalkoDev.WebAPI.Models.Auth
{
	public class JWTInfo
	{
		public JWTInfo(string jwt, DateTime expires)
		{
			JWT = jwt;
			Expires = expires;
		}

		public string JWT
		{
			get;
			private set;
		}

		public DateTime Expires
		{
			get;
			private set;
		}

	}
}

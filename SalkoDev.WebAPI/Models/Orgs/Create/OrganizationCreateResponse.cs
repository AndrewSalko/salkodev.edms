
namespace SalkoDev.WebAPI.Models.Orgs.Create
{
	public class OrganizationCreateResponse: BaseResponse
	{
		public OrganizationCreateResponse()
		{
		}

		public string UID
		{
			get;
			set;
		}

		public OrganizationCreateResponse(string error, bool success)
		{
			Errors = new List<string> { error };
			Success = success;
		}

	}
}

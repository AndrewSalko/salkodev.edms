using SalkoDev.WebAPI.Models.Search;

namespace SalkoDev.WebAPI.Models.OrgMembers
{
	public class OrganizationMemberSearchParams
	{
		public string OrganizationUID
		{
			get;
			set;
		}

		/// <summary>
		/// Адрес Email пользователя, который на данный момент связан с данной организацией
		/// </summary>
		public string Email
		{
			get;
			set;
		}

		public SearchOption EmailOption
		{
			get;
			set;
		}


		public string Name
		{
			get;
			set;
		}

		public SearchOption NameOption
		{
			get;
			set;
		}


	}
}

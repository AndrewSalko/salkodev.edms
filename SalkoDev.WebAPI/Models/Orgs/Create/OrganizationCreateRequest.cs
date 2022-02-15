using System.ComponentModel.DataAnnotations;

namespace SalkoDev.WebAPI.Models.Orgs.Create
{
	/// <summary>
	/// Запрос для создания новой организации
	/// </summary>
	public class OrganizationCreateRequest
	{

		/// <summary>
		/// Имя организации (отображаемое краткое)
		/// </summary>
		[Required]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Полное имя организации. Если не задано, будет взято Name
		/// </summary>
		public string FullName
		{
			get;
			set;
		}
	}
}

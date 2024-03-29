
namespace SalkoDev.WebAPI.Models.Counterparties
{
	public class Counterparty
	{

		public string UID
		{
			get;
			set;
		}

		/// <summary>
		/// Наименование (краткое имя)
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Полное наименование контрагента
		/// </summary>
		public string FullName
		{
			get;
			set;
		}


	}
}

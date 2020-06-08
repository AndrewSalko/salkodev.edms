using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalkoDev.Organization.Data
{
	public class Organization
	{
		/// <summary>
		/// Наименование (имя)
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Полное наименование
		/// </summary>
		public string FullName
		{
			get;
			set;
		}

		/// <summary>
		/// Описание
		/// </summary>
		public string Description
		{
			get;
			set;
		}


		/// <summary>
		/// Физич.адрес
		/// </summary>
		public string PhysicalAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Юридический адрес
		/// </summary>
		public string LegalAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Код организации (ЕДРПОУ)
		/// </summary>
		public string OrganizationCode
		{
			get;
			set;
		}

		/// <summary>
		/// Контакты
		/// </summary>
		public Contact[] Contacts
		{
			get;
			set;
		}
		


	}
}

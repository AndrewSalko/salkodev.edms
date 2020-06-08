using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SalkoDev.Organization.Data
{
	/// <summary>
	/// Контакт (ел.почта, телефон и прочее)
	/// </summary>
	public class Contact
	{
		/// <summary>
		/// Тип контакта
		/// </summary>
		public ContactType ValueType
		{
			get;
			set;
		}

		/// <summary>
		/// Значение (здесь email-адрес, или телефон)
		/// </summary>
		public string Value
		{
			get;
			set;
		}

		/// <summary>
		/// Имя (для случая если email - здесь может быть Имя человека, отображаемое)
		/// </summary>
		public string Name
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


	}
}

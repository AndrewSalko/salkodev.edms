using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;


namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Organizations
{
	/// <summary>
	/// Организация. Ключ партиции - UID
	/// </summary>
	public class Organization
	{

		/// <summary>
		/// Техническое поле Id
		/// </summary>
		[BsonId]
		public MongoDB.Bson.ObjectId Id
		{
			get;
			set;
		}

		/// <summary>
		/// Уник.идентификатор организации (не связанный с техническим полем Id)
		/// Ключ партиции.
		/// Строковое представление GUID (?)
		/// </summary>
		[BsonRequired]
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
		/// Полное наименование организации
		/// </summary>
		public string FullName
		{
			get;
			set;
		}

		/// <summary>
		/// Пользователь, который создал эту организацию (либо тот, кто ею сейчас владеет)
		/// TODO@: удобно ли будет делать поиск такого юзера? Ведь партиция у нас - Email
		/// </summary>
		public string OwnerUserUID
		{
			get;
			set;
		}

	}
}

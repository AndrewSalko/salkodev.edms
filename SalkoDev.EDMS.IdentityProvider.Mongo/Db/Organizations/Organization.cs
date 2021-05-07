using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;


namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Organizations
{
	/// <summary>
	/// TODO@: выбрать ключ партицирования
	/// </summary>
	public class Organization
	{

		/// <summary>
		/// Уник.идентификатор организации
		/// </summary>
		[BsonId]
		public MongoDB.Bson.ObjectId Id
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


	}
}

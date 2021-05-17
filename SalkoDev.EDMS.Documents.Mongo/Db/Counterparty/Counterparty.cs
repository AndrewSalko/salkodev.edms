using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;


namespace SalkoDev.EDMS.Documents.Mongo.Db.Counterparty
{
	/// <summary>
	/// Контрагент (корреспондент)
	/// </summary>
	public class Counterparty
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
		/// Ключ партиции - организация, которая владеет этими записями. В нормальной работе даже если
		/// записей-контрагентов будет даже 100-300к это будет мизерный объем.
		/// </summary>
		public string OrganizationUID
		{
			get;
			set;
		}

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
		/// Полное наименование контрагента
		/// </summary>
		public string FullName
		{
			get;
			set;
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Organizations.Members
{
	/// <summary>
	/// Пользователь - член организации.
	/// Ключ партиции OrganizationUID.
	/// </summary>
	public class OrganizationMember
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
		/// </summary>
		[BsonRequired]
		public string OrganizationUID
		{
			get;
			set;
		}

		/// <summary>
		/// Адрес Email пользователя, который на данный момент связан с данной организацией
		/// </summary>
		[BsonRequired]
		public string Email
		{
			get;
			set;
		}

		/// <summary>
		/// Имя пользователя (оно должно быть синхронизировано с тем, что в Users),
		/// но здесь оно для удобства - чтобы легко выводить юзеров списом или даже искать по имени
		/// </summary>
		[BsonRequired]
		public string Name
		{
			get;
			set;
		}


	}
}

using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users
{
	public class User : IIdentity, IUser
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
		/// Отображаемое имя пользователя (атрибут не связан с системой IIdentity)
		/// </summary>
		public string UserName
		{
			get;
			set;
		}

		/// <summary>
		/// Организация, к которой привязан пользователь (ее можно задать после регистрации).
		/// Если поле пустое - можно создать свою организацию. Если заполнено - юзер в ней.
		/// </summary>
		public string OrganizationUID
		{
			get;
			set;
		}

		/// <summary>
		/// (Атрибут от IIdentity)
		/// </summary>
		[BsonIgnore]
		public string AuthenticationType
		{
			get;
			set;
		}

		/// <summary>
		/// (Атрибут от IIdentity)
		/// </summary>
		[BsonIgnore]
		public bool IsAuthenticated
		{
			get;
			set;
		}

		/// <summary>
		/// (Атрибут от IIdentity). У нас не применяется вообще - его нельзя использовать для "отображаемого имени", там контроль символов
		/// </summary>
		[BsonIgnore]
		public string Name => Email;

		/// <summary>
		/// Нормализованное имя. Подумать, возможно от этого можно будет уйти если поменять нормализатор
		/// </summary>
		[BsonIgnore]
		public string NormalizedUserName => Name;

		/// <summary>
		/// Это логин (основное поле). Он же будет ключом партицирования.
		/// TODO@: в нашей модели нельзя менять Email-адрес, т.к. это ключ партиции. Это удобно чтобы делать быстро поиск юзера для логина, но плохо для смены адреса.
		/// UID - юзера сейчас эту задачу "решает" это поле
		/// </summary>
		[BsonRequired]
		public string Email
		{
			get;
			set;
		}

		/// <summary>
		/// Уникальный идентификатор юзера. Сейчас это возвращает поле Email (оно уникальное и ключ партиции)
		/// </summary>
		[BsonIgnore]
		public string UID => Email;

		public bool EmailConfirmed
		{
			get;
			set;
		}

		public string PasswordHash
		{
			get;
			set;
		}

	}
}

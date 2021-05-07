using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace SalkoDev.EDMS.IdentityProvider.Mongo
{
	public class User : IIdentity
	{
		/// <summary>
		/// Отображаемое имя пользователя (атрибут не связан с системой IIdentity)
		/// </summary>
		public string UserName
		{
			get;
			set;
		}

		/// <summary>
		/// Организация, к которой привязан пользователь (ее можно задать после регистрации)
		/// </summary>
		public MongoDB.Bson.ObjectId OrganizationID
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
		/// Уник.идентификатор юзера
		/// </summary>
		[BsonId]
		public MongoDB.Bson.ObjectId Id
		{
			get;
			set;
		}

		/// <summary>
		/// Это логин (основное поле). Он же будет ключом партицирования
		/// </summary>
		[BsonRequired]
		public string Email
		{
			get;
			set;
		}

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

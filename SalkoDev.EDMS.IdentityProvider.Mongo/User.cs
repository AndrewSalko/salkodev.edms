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
		/// (Атрибут от IIdentity). У нас не применяется вообще.
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

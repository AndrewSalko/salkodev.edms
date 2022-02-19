using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users
{
	public interface IUser: IIdentity
	{
		/// <summary>
		/// Техническое поле Id
		/// </summary>
		MongoDB.Bson.ObjectId Id
		{
			get;
			set;
		}


		string Email
		{
			get;
		}

		/// <summary>
		/// Организация, к которой привязан пользователь (ее можно задать после регистрации).
		/// Если поле пустое - можно создать свою организацию. Если заполнено - юзер в ней.
		/// </summary>
		string OrganizationUID
		{
			get;
		}

		/// <summary>
		/// Уникальный идентификатор юзера. Сейчас это возвращает поле Email (оно уникальное и ключ партиции)
		/// </summary>
		string UID
		{
			get;
		}

		bool EmailConfirmed
		{
			get;
			set;
		}


	}
}

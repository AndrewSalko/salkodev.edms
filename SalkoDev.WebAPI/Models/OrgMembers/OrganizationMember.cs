namespace SalkoDev.WebAPI.Models.OrgMembers
{
	/// <summary>
	/// TODO: как быть с разными классами здесь (в моделях) и в провайдере Mongo
	/// </summary>
	public class OrganizationMember
	{
		/// <summary>
		/// Уник.идентификатор организации (не связанный с техническим полем Id)
		/// Ключ партиции.
		/// </summary>

		public string OrganizationUID
		{
			get;
			set;
		}

		/// <summary>
		/// Адрес Email пользователя, который на данный момент связан с данной организацией
		/// </summary>
		public string Email
		{
			get;
			set;
		}

		/// <summary>
		/// Имя пользователя (оно должно быть синхронизировано с тем, что в Users),
		/// но здесь оно для удобства - чтобы легко выводить юзеров списом или даже искать по имени
		/// </summary>
		public string Name
		{
			get;
			set;
		}

	}
}

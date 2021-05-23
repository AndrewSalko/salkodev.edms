using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users;
using Microsoft.AspNetCore.Identity;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db.Organizations;
using SalkoDev.WebAPI.Models.Auth.Login;

namespace SalkoDev.WebAPI.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CounterpartiesController: ControllerBase
	{
		readonly UserManager<User> _UserManager;
		readonly IOrganizationStore _OrganizationStore;

		static Models.Counterparties.Counterparty[] _TEST_COUNTERPARTIES = new Models.Counterparties.Counterparty[]
		{
			new Models.Counterparties.Counterparty()
			{
				FullName="ТОВ Фосс-Он-Лайн",
				Name="Фосс-Он-Лайн",
				UID="uid1"
			},

			new Models.Counterparties.Counterparty()
			{
				FullName="ПАТ Мегабанк",
				Name="Мегабанк",
				UID="uid2"
			},

			new Models.Counterparties.Counterparty()
			{
				FullName="Кабінет Міністрів України",
				Name="КМУ",
				UID="uid3"
			},
		};


		public CounterpartiesController(UserManager<User> userManager, IOrganizationStore organizationStore)  //, IUserStoreEx userStoreEx
		{
			_UserManager = userManager;
			_OrganizationStore = organizationStore;
		}

		/// <summary>
		/// Получение списка контрагентов
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Models.Counterparties.Counterparty> Get()
		{
			//TODO@: такой метод не имеет смысла т.к. нужна пагинация и лимиты. Без них данных может быть слишком много
			//возможно здесь мы вернем список последних по дате создания записей, по дефолт-лимиту и все

			//limit=25&offset=50

			return _TEST_COUNTERPARTIES;
		}


		[HttpGet("{uid}")]
		public async Task<IActionResult> GetItemByUID(string uid)
		{
			//Task<ActionResult<Models.Counterparties.Counterparty>>

			//Найти юзера по авторизац.токену. claimsPrincipal.Identity тут не помог (там визуально ничего нет)
			var user = await UserFromClaim.GetUser(_UserManager, HttpContext.User);
			if (user == null)
				return BadRequest(UserLoginResponse.Failed(Resource.InvalidLoginRequest));

			//Если пользователь не состоит в организации, то доступ к справочникам и документам ему закрыт - вначале организацию надо или создать,
			//или вступить в одну из существующих
			if (string.IsNullOrEmpty(user.OrganizationUID))
			{
				return BadRequest(UserLoginResponse.Failed(Resource.UserIsNotAMemberOfAnyOrganization));
			}

			//TODO@: у тек.пользователя взять организацию OrgUID, она критически необходима для поиска

			//var todoItem = await _context.TodoItems.FindAsync(id);

			//if (todoItem == null)
			//{
			//	return NotFound();
			//}

			//return todoItem;

			return NotFound();
		}



	}
}

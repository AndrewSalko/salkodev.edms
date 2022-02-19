using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using SalkoDev.WebAPI.Models.Orgs.Create;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db.Organizations;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users;

namespace SalkoDev.WebAPI.Controllers
{
	/// <summary>
	/// Управление организациями. Пользователь, чья регистрация проведена полностью (с подвт.email) может создать
	/// свою организацию, либо вступить по приглашению в одну из существующих.
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class OrgsController : ControllerBase
	{
		readonly UserManager<User> _UserManager;
		readonly IUserStoreEx _UserStoreEx;
		readonly IOrganizationStore _OrganizationStore;

		public OrgsController(UserManager<User> userManager, IOrganizationStore organizationStore, IUserStoreEx userStoreEx)
		{
			_UserManager = userManager;
			_OrganizationStore = organizationStore;
			_UserStoreEx = userStoreEx;
		}


		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create([FromBody] OrganizationCreateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(new OrganizationCreateResponse(Resource.InvalidPayload, false));

			//Найти юзера по авторизац.токену. claimsPrincipal.Identity тут не помог (там визуально ничего нет)
			var user = await UserFromClaim.GetUser(_UserManager, HttpContext.User);
			if (user == null)
				return BadRequest(new OrganizationCreateResponse(Resource.InvalidLoginRequest, false));

			if(!user.EmailConfirmed)
				return BadRequest(new OrganizationCreateResponse(Resource.EmailNotConfirmed, false));

			//Если пользователь состоит в организации создавать новую нельзя. Чтобы создать,
			//нужно вначале покинуть организацию. Если он создает, то автоматом становится ее членом.
			if (!string.IsNullOrEmpty(user.OrganizationUID))
				return BadRequest(new OrganizationCreateResponse(Resource.UserIsMemberOfOrganization, false));

			var org = await _OrganizationStore.Create(request.Name, request.FullName, user.UID);

			//также нужно прописать в юзера в свойство что он уже создал организацию...
			//TODO@: плохо с транзакционностью - можно создать организацию, но упасть на изменении юзера (не пропишется ему свойство)

			await _UserStoreEx.SetUserOrganizationAsync(user, org.UID);

			return Ok(new OrganizationCreateResponse()
			{
				Success = true,
				UID = org.UID
			});


		}


	}
}

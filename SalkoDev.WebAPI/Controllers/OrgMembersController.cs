using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using SalkoDev.WebAPI.Models.OrgMembers;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users;
using SalkoDev.WebAPI.Models.Auth.Login;

namespace SalkoDev.WebAPI.Controllers
{
	/// <summary>
	/// Получение пользователей-членов организации (по ее UID), с возможностью фильтрации по параметрам
	/// Назначение этого контроллера - если я вошел в систему, и мне нужно получить список моих коллег из моей же организации
	/// </summary>
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class OrgMembersController : ControllerBase
	{
		readonly UserManager<User> _UserManager;

		public OrgMembersController(UserManager<User> userManager)
		{
			_UserManager = userManager;
		}


		/// <summary>
		/// Получение списка членов организации по ее UID, с возможностью опционально задать параметры для поиска
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> GetMembers(OrganizationMemberSearchParams options)
		{
			var user = await UserFromClaim.GetUser(_UserManager, HttpContext.User);
			if (user == null)
				return BadRequest(UserLoginResponse.Failed(Resource.InvalidLoginRequest));

			if (string.IsNullOrEmpty(user.OrganizationUID))
			{
				return BadRequest(UserLoginResponse.Failed(Resource.UserIsNotAMemberOfAnyOrganization));
			}

			string orgUID = options.OrganizationUID;

			//TODO@: реализовать получение списка сотрудников по фильтру из базы

			OrganizationMember m1 = new OrganizationMember
			{
				Email = "borris@gmail.com",
				OrganizationUID = orgUID,
				Name = "Borris Johnson"
			};

			OrganizationMember m2 = new OrganizationMember
			{
				Email = "biden@usa.gov",
				OrganizationUID = orgUID,
				Name = "John Biden"
			};


			OrganizationMember[] result = { m1, m2 };

			return	CreatedAtAction(nameof(GetMembers), result);

			//return NotFound();
		}

		

	}
}

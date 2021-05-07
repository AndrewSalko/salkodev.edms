using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using SalkoDev.EDMS.IdentityProvider.Mongo;
using SalkoDev.WebAPI.Configuration;
using SalkoDev.WebAPI.Models.Auth;
using SalkoDev.WebAPI.Models.Auth.ChangePassword;
using SalkoDev.WebAPI.Models.Auth.Confirmation;
using SalkoDev.WebAPI.Models.Auth.Login;
using SalkoDev.WebAPI.Models.Auth.Registration;
using Microsoft.AspNetCore.Authorization;
using SalkoDev.WebAPI.Models.Orgs.Create;

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

		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create([FromBody] OrganizationCreateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(new RegistrationResponse(Resource.InvalidPayload, false));

			throw new NotImplementedException();

			//var existingUser = await _UserManager.FindByEmailAsync(request.Email);

			//if (existingUser != null)
			//{
			//	return BadRequest(new RegistrationResponse(Resource.EmailAlreadyInUse, false));
			//}

			//var newUser = new User()
			//{
			//	Email = request.Email,
			//	UserName = request.Name
			//};

			//var isCreated = await _UserManager.CreateAsync(newUser, request.Password);
			//if (isCreated.Succeeded)
			//{
			//	string emailConfirmToken = await _UserManager.GenerateEmailConfirmationTokenAsync(newUser);

			//	//отправить его на заданный адрес почты
			//	string htmlBody = $"emailConfirmToken: {emailConfirmToken}";

			//	await _EmailSender.SendEmailAsync(request.Email, "Confirm registration", htmlBody);

			//	var jwtToken = _GenerateJwtToken(newUser);

			//	return Ok(new RegistrationResponse()
			//	{
			//		Success = true,
			//		Token = jwtToken.JWT,
			//		Expires = jwtToken.Expires
			//	});
			//}
			//else
			//{
			//	return BadRequest(new RegistrationResponse()
			//	{
			//		Errors = isCreated.Errors.Select(x => x.Description).ToList(),
			//		Success = false
			//	});
			//}
		}




	}
}

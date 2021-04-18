using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalkoDev.EDMS.IdentityProvider.Mongo;
using SalkoDev.WebAPI.Configuration;
using SalkoDev.WebAPI.Models.Auth;
using SalkoDev.WebAPI.Models.Auth.ChangePassword;
using SalkoDev.WebAPI.Models.Auth.Login;
using SalkoDev.WebAPI.Models.Auth.Registration;

namespace SalkoDev.WebAPI
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		readonly UserManager<User> _UserManager;
		readonly JWTConfig _JWTConfig;

		public AuthController(UserManager<User> userManager, IOptionsMonitor<JWTConfig> optionsMonitor)
		{
			_UserManager = userManager;
			_JWTConfig = optionsMonitor.CurrentValue;
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(new RegistrationResponse(Resource.InvalidPayload, false));

			var existingUser = await _UserManager.FindByEmailAsync(request.Email);

			if (existingUser != null)
			{
				return BadRequest(new RegistrationResponse(Resource.EmailAlreadyInUse, false));
			}

			var newUser = new User() { Email = request.Email };
			var isCreated = await _UserManager.CreateAsync(newUser, request.Password);
			if (isCreated.Succeeded)
			{
				var jwtToken = _GenerateJwtToken(newUser);

				return Ok(new RegistrationResponse()
				{
					Success = true,
					Token = jwtToken.JWT,
					Expires = jwtToken.Expires
				});
			}
			else
			{
				return BadRequest(new RegistrationResponse()
				{
					Errors = isCreated.Errors.Select(x => x.Description).ToList(),
					Success = false
				});
			}
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(new RegistrationResponse(Resource.InvalidPayload, false));

			var existingUser = await _UserManager.FindByEmailAsync(request.Email);
			if (existingUser == null)
			{
				return BadRequest(new RegistrationResponse(Resource.InvalidLoginRequest, false));
			}

			var isCorrect = await _UserManager.CheckPasswordAsync(existingUser, request.Password);
			if (!isCorrect)
			{
				return BadRequest(new RegistrationResponse(Resource.InvalidLoginRequest, false));
			}

			var jwtToken = _GenerateJwtToken(existingUser);

			return Ok(new RegistrationResponse()
			{
				Success = true,
				Token = jwtToken.JWT,
				Expires=jwtToken.Expires
			});
		}

		[HttpPost]
		[Route("ChangePassword")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(new RegistrationResponse(Resource.InvalidPayload, false));

			var existingUser = await _UserManager.FindByEmailAsync(request.Email);
			if (existingUser == null)
			{
				return BadRequest(new RegistrationResponse(Resource.InvalidLoginRequest, false));
			}

			//юзер менеджер сам сверит текущий пароль
			var result = await _UserManager.ChangePasswordAsync(existingUser, request.CurrentPassword, request.NewPassword);

			if (!result.Succeeded)
			{
				return BadRequest(new ChangePasswordResponse()
				{
					Errors = result.Errors.Select(x => x.Description).ToList(),
					Success = false
				});
			}

			return Ok(new ChangePasswordResponse()
			{
				Success = true
			});
		}


		JWTInfo _GenerateJwtToken(User user)
		{
			//https://www.c-sharpcorner.com/article/authentication-and-authorization-in-asp-net-5-with-jwt-and-swagger/

			var authClaims = new Claim[]
			{
				new Claim("Id", user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var key = Encoding.ASCII.GetBytes(_JWTConfig.Secret);
			var signCreds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(
				//	issuer: _configuration["JWT:ValidIssuer"],
				//	audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddHours(48),
				claims: authClaims,
				signingCredentials: signCreds
				);

			//token.

			var jwtTokenHandler = new JwtSecurityTokenHandler();

			var jwtToken = jwtTokenHandler.WriteToken(token);

			var result = new JWTInfo(jwtToken, token.ValidTo);
			return result;
		}

	}
}

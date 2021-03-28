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
			if (ModelState.IsValid)
			{
				var existingUser = await _UserManager.FindByEmailAsync(request.Email);

				if (existingUser != null)
				{
					return BadRequest(new RegistrationResponse("Email already in use", false));
				}

				var newUser = new User() { Email = request.Email };
				var isCreated = await _UserManager.CreateAsync(newUser, request.Password);
				if (isCreated.Succeeded)
				{
					var jwtToken = _GenerateJwtToken(newUser);

					return Ok(new RegistrationResponse()
					{
						Success = true,
						Token = jwtToken
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

			return BadRequest(new RegistrationResponse("Invalid payload", false));
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
		{
			if (ModelState.IsValid)
			{
				var existingUser = await _UserManager.FindByEmailAsync(user.Email);

				if (existingUser == null)
				{
					return BadRequest(new RegistrationResponse("Invalid login request", false));
				}

				var isCorrect = await _UserManager.CheckPasswordAsync(existingUser, user.Password);

				if (!isCorrect)
				{
					return BadRequest(new RegistrationResponse("Invalid login request", false));
				}

				var jwtToken = _GenerateJwtToken(existingUser);

				return Ok(new RegistrationResponse()
				{
					Success = true,
					Token = jwtToken
				});
			}

			return BadRequest(new RegistrationResponse("Invalid payload", false));
		}

		string _GenerateJwtToken(User user)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			var key = Encoding.ASCII.GetBytes(_JWTConfig.Secret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("Id", user.Id.ToString()),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim(JwtRegisteredClaimNames.Sub, user.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(6),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = jwtTokenHandler.CreateToken(tokenDescriptor);
			var jwtToken = jwtTokenHandler.WriteToken(token);

			return jwtToken;
		}



	}
}

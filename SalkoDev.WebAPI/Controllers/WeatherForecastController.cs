using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using SalkoDev.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalkoDev.WebAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		readonly ILogger<WeatherForecastController> _Logger;

		// The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
		static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_Logger = logger;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			//TODO@: возможно это стоит изучить
			//HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}

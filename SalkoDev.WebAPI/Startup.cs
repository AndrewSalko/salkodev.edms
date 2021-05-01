using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SalkoDev.EDMS.IdentityProvider.Mongo;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db;
using SalkoDev.WebAPI.Configuration;
using SalkoDev.EmailSend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalkoDev.WebAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add identity types - User and Role (from EDMS.IdentityProvider.Mongo)
			services.AddIdentity<User, Role>().AddDefaultTokenProviders();

			string connString = "mongodb://localhost:27017/?readPreference=primary&appname=salkodev&ssl=false";
			string dbName = "salkodev";

			services.AddTransient<IDatabase>(db => new Database(connString, false, dbName));

			services.AddTransient<IEmailSender>(emailSender => new EmailSender());

			// Identity Services
			services.AddTransient<IUserStore<User>, UserStore>();
			services.AddTransient<IRoleStore<Role>, RoleStore>();

			//string connectionString = Configuration.GetConnectionString("DefaultConnection");
			//services.AddTransient<SqlConnection>(e => new SqlConnection(connectionString));

			//----------- sample -

			//see appsettings.json
			services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

			//требуем для входа подтв.адрес Email
			services.Configure<IdentityOptions>(opts =>
			{
				opts.SignIn.RequireConfirmedEmail = true;
			});

			//sqlite
			//services.AddDbContext<ApiDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(jwt =>
			{
				var key = Encoding.ASCII.GetBytes(Configuration["JWTConfig:Secret"]);

				jwt.SaveToken = true;
				jwt.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					RequireExpirationTime = false
				};
			});


			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "SalkoDev.WebAPI", Version = "v1" });

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT: enter 'Bearer <yourtoken>'."
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							new string[] {}
					}
				});

			});
		}



		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalkoDev.WebAPI v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

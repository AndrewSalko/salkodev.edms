using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SalkoDev.EmailSend
{
	/// <summary>
	/// Для отправки email с ссылкой-подтверждением регистрации
	/// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-5.0&tabs=visual-studio
	/// </summary>
	public class EmailSender : IEmailSender
	{
		public EmailSender()
		{

		}

		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{

			return Task.CompletedTask;
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
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
			//TODO@: реализовать отправку Email-подтверждения, пока только запись в лог

			string logDir = Environment.GetEnvironmentVariable("salkodev_edms_logs");
			if (!string.IsNullOrEmpty(logDir))
			{
				string fileName = string.Format("{0:dd.MM.yyyy_HH_mm_ss}_{1}.log", DateTime.Now, email);
				string fullName = Path.Combine(logDir, fileName);

				string text = $"{email} - subject:{subject}";

				using (var fs = File.AppendText(fullName))
				{
					fs.WriteLine(text);
					fs.WriteLine(htmlMessage);
				}
			}

			return Task.CompletedTask;
		}
	}
}

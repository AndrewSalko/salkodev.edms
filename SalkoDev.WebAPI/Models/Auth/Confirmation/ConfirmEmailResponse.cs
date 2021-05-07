using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalkoDev.WebAPI.Models.Auth.Confirmation
{
	public class ConfirmEmailResponse : BaseResponse
	{
		public ConfirmEmailResponse()
		{
		}

		public ConfirmEmailResponse(string error, bool success)
		{
			Errors = new List<string> { error };
			Success = success;
		}

	}
}

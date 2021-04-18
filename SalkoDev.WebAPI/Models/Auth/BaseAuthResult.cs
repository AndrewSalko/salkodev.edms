using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalkoDev.WebAPI.Models.Auth
{
	public abstract class BaseAuthResult
	{
		public bool Success
		{
			get;
			set;
		}
		public List<string> Errors
		{
			get;
			set;
		}

	}
}

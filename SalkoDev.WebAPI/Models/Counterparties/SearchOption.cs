using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SalkoDev.WebAPI.Models.Counterparties
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum SearchOption
	{
		Equals = 1,
		Contains = 2,
		StartsWith = 3
	}
}

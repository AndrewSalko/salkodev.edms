using System.Text.Json.Serialization;

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

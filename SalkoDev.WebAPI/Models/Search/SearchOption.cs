using System.Text.Json.Serialization;

namespace SalkoDev.WebAPI.Models.Search
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum SearchOption
	{
		Unspecified = 0,
		Equals = 1,
		Contains = 2,
		StartsWith = 3
	}
}

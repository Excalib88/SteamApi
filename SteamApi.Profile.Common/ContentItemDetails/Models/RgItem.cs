using System.Text.Json.Serialization;

namespace SteamApi.Profile.Common.ContentItemDetails.Models
{
	public class RgItem
	{
		public string Id { get; set; }

		public string ClassId { get; set; }

		public string InstanceId { get; set; }

		public string Amount { get; set; }

		[JsonPropertyName("hide_in_china")]
		public int HideInChina { get; set; }

		[JsonPropertyName("pos")]
		public int Position { get; set; }
	}
}
using Newtonsoft.Json;

namespace SteamApi.Profile.Common.ContentItemDetails.Models
{
	[JsonObject]
	public class RgDescription
	{
		public string AppId { get; set; }
		public string ClassId { get; set; }
		public string InstanceId { get; set; }

		[JsonProperty("icon_url")]
		public string IconUrl { get; set; }

		public string Name { get; set; }

		public int Tradable { get; set; }
		
		public int Marketable { get; set; }
	}
}
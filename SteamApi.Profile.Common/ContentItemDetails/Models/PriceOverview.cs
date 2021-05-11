using Newtonsoft.Json;

namespace SteamApi.Profile.Common.ContentItemDetails.Models
{
	public class PriceOverview
	{
		public bool Success { get; set; }
		[JsonProperty("lowest_price")]
		public string LowestPrice { get; set; }
		public string Volume { get; set; }
		[JsonProperty("median_price")]
		public string MedianPrice { get; set; }
	}
}
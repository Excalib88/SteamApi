using System.Collections.Generic;
using System.Text.Json.Serialization;
using SteamApi.Profile.Common.Helpers;

namespace SteamApi.Profile.Common.ContentItemDetails.Models
{
	public class RgInventoryResponse
	{
		public bool Success { get; set; }
		[JsonPropertyName("rgInventory")]
		public Dictionary<string, RgItem> RgInventory { get; set; }
		
		//public string[] RgCurrency{get;set;} 
		[JsonPropertyName("rgDescriptions")]
		[JsonConverter(typeof(DictionaryConverter))]
		public Dictionary<string, RgDescription> RgDescriptions { get; set; }
	}
}
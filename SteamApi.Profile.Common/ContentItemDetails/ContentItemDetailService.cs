using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SteamApi.Profile.Common.ContentItemDetails.Models;
using SteamApi.Profile.Common.Exceptions;
using SteamApi.Profile.Common.Helpers;

namespace SteamApi.Profile.Common.ContentItemDetails
{
	public class ContentItemDetailService : IContentItemDetailService
	{
		private readonly HttpClient _profileHttpClient;

		public ContentItemDetailService(IHttpClientFactory profileClientFactory)
		{
			_profileHttpClient = profileClientFactory.CreateClient("SteamProfile");
		}

		public async Task<RgDescription> GetItemDetailByItemId(string itemId, string steamId)
		{
			var response = await _profileHttpClient.GetAsync($"/profiles/{steamId}/inventory/json/570/2");

			if (response.IsSuccessStatusCode)
			{
				var stringContent = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<RgInventoryResponse>(stringContent)
				             ?? throw new Exception("RgInventory not found");

				var item = result.RgInventory[itemId] ?? throw new Exception("Item not found");
				var key = item.ClassId + "_" + item.InstanceId;
				var rgDescription = result.RgDescriptions[key] ?? throw new Exception("RgDescription not found");
				rgDescription.IconUrl = UrlConstants.ImageServiceUrl + rgDescription.IconUrl;

				return rgDescription;
			}

			if (response.StatusCode == HttpStatusCode.TooManyRequests)
			{
				await Task.Delay(1000);
				await GetItemDetailByItemId(itemId, steamId);
			}

			return null;
		}
		
		public async Task<List<RgDescription>> GetItemDetails(List<string> itemIds, string steamId)
		{
			var response = await _profileHttpClient.GetAsync($"/profiles/{steamId}/inventory/json/570/2");

			if (response.IsSuccessStatusCode)
			{
				var stringContent = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<RgInventoryResponse>(stringContent) 
							?? throw new ItemNotFoundException("RgInventory not found");

				var resultCollection = new List<RgDescription>();

				foreach (var itemId in itemIds)
				{
					var item = result.RgInventory[itemId] ?? throw new ItemNotFoundException("Item not found");
					var key = item.ClassId + "_" + item.InstanceId;
					var rgDescription = result.RgDescriptions[key] ?? throw new ItemNotFoundException("RgDescription not found");
					rgDescription.IconUrl = UrlConstants.ImageServiceUrl + rgDescription.IconUrl;
					
					resultCollection.Add(rgDescription);
				}

				return resultCollection;
			}

			if (response.StatusCode == HttpStatusCode.TooManyRequests)
			{
				await Task.Delay(1000);
				await GetItemDetails(itemIds, steamId);
			}

			return null;
		}

		public async Task<List<string>> GetItemIdsBySteamId(string steamId)
		{
			var response = await _profileHttpClient.GetAsync($"/profiles/{steamId}/inventory/json/570/2");

			if (response.IsSuccessStatusCode)
			{
				var stringContent = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<RgInventoryResponse>(stringContent) ?? 
								throw new ItemNotFoundException("RgInventory not found");
				return result.RgInventory.Keys.ToList();
			}

			if(response.StatusCode == HttpStatusCode.TooManyRequests)
			{
				await Task.Delay(1000);
				await GetItemIdsBySteamId(steamId);
			}

			return null;
		}

		public async Task<PriceOverview> GetPrice(int appId, string marketHashName)
		{
			marketHashName = marketHashName.Replace(" ", "%20");
			var response = await _profileHttpClient.GetAsync(
				$"https://steamcommunity.com/market/priceoverview/?appid={appId}&currency=1&market_hash_name={marketHashName}");

			if(response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<PriceOverview>(content) ?? 
				             throw new ItemNotFoundException("Price not found");

				return result;
			}

			if(response.StatusCode == HttpStatusCode.TooManyRequests)
			{
				await Task.Delay(1000);
				await GetPrice(appId, marketHashName);
			}

			return null;
		}

		public async Task<Page<List<ItemPrice>>> GetPricesBySteamId(string steamId, int take, int skip)
		{
			var itemIds = await GetItemIdsBySteamId(steamId);

			if (itemIds == null || !itemIds.Any())
			{
				throw new Exception("Item ids null");
			}
			
			var items = await GetItemDetails(itemIds, steamId);
			
			if (items == null || !items.Any())
			{
				throw new Exception("Items null");
			}
			var count = items.Count(x => x.Marketable == 1 && x.Tradable == 1);

			var filteredItems = items.Where(x => x.Marketable == 1 && x.Tradable == 1)
				.Take(take).Skip(skip).ToList();

			if (filteredItems == null || !filteredItems.Any())
			{
				throw new Exception("FilteredItems ids null");
			}
			
			var prices = new List<ItemPrice>();
			foreach (var item in filteredItems)
			{
				var price = await GetPrice(Convert.ToInt32(item.AppId), item.Name);

				if(price.LowestPrice == null) continue;

				var priceNumber = price.LowestPrice.Replace("$", "").Replace(".", ",");
				var isParsePrice = decimal.TryParse(priceNumber, out var priceDecimal);
				
				if(!isParsePrice) continue;
				
				prices.Add(new ItemPrice
				{
					Price = priceDecimal,
					ItemName = item.Name,
					Icon = item.IconUrl
				});
			}

			return new Page<List<ItemPrice>>(prices, count);
		}
	}
}
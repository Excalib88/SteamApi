using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SteamApi.Profile.Common.ContentItemDetails.Models;
using SteamApi.Profile.Common.Exceptions;
using SteamApi.Profile.Common.UserProfiles;
using SteamApi.Profile.Common.UserProfiles.Models;

namespace SteamApi.Profile.Common
{
	public class UserProfileService : IUserProfileService
	{
		private readonly HttpClient _apiHttpClient;

		public UserProfileService(IHttpClientFactory clientFactory)
		{
			_apiHttpClient = clientFactory.CreateClient("SteamApi");
		}

		public async Task<UserProfile> GetUserInfo(string steamId)
		{
			var response = await _apiHttpClient.GetAsync($"/ISteamUser/GetPlayerSummaries/v0002/?key=040D960C5FB4643C124F8B9EA8BAD4AA&steamids={steamId}");

			if (response.IsSuccessStatusCode)
			{
				var stringContent = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<RgInventoryResponse>(stringContent) ?? 
							throw new ItemNotFoundException("RgInventory not found");
			}

			return null;
		}
	}
}
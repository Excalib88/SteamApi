using System.Collections.Generic;
using System.Threading.Tasks;
using SteamApi.Profile.Common.ContentItemDetails.Models;
using SteamApi.Profile.Common.Helpers;

namespace SteamApi.Profile.Common.ContentItemDetails
{
	public interface IContentItemDetailService
	{
		Task<RgDescription> GetItemDetailByItemId(string itemId, string steamId);
		Task<List<RgDescription>> GetItemDetails(List<string> itemId, string steamId);
		Task<List<string>> GetItemIdsBySteamId(string steamId);
		Task<PriceOverview> GetPrice(int appId, string marketHashName);
		Task<Page<List<ItemPrice>>> GetPricesBySteamId(string steamId, int take, int skip);
	}
}
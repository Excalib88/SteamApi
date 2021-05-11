using System.Threading.Tasks;
using SteamApi.Profile.Common.UserProfiles.Models;

namespace SteamApi.Profile.Common.UserProfiles
{
	public interface IUserProfileService
	{
		Task<UserProfile> GetUserInfo(string steamId);
	}
}
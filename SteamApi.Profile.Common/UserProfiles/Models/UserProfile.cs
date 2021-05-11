using Newtonsoft.Json;

namespace SteamApi.Profile.Common.UserProfiles.Models
{
	public class UserProfile
	{
		[JsonProperty("steamid")]
		public string SteamId { get; set; }
		
		[JsonProperty("communityvisibilitystate")]
		public int CommunityVisibilityState { get; set; }
		
		[JsonProperty("profilestate")]
		public int ProfileState { get; set; }
		
		[JsonProperty("personaname")]
		public string PersonName { get; set; }
		
		[JsonProperty("commentpermission")]
		public int CommentPermission { get; set; }
		
		[JsonProperty("profileurl")]
		public string ProfileUrl { get; set; }
		
		[JsonProperty("avatar")]
		public string AvatarUrl { get; set; }
		
		[JsonProperty("avatarmedium")]
		public string AvatarMediumUrl { get; set; }
		
		[JsonProperty("avatarfull")]
		public string AvatarFullUrl { get; set; }
		
		[JsonProperty("avatarhash")]
		public string AvatarHash { get; set; }
	}
}
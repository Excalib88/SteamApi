using System;

namespace SteamApi.Profile.Common.Exceptions
{
	public class ItemNotFoundException : Exception
	{
		public ItemNotFoundException(string message) : base(message)
		{
		}
	}
}
namespace SteamApi.Profile.Common.Helpers
{
	public class Page<TModel>
	{
		public Page()
		{
		}

		public Page(TModel data, int count)
		{
			Data = data;
			Count = count;
		}

		public TModel Data { get; set; }
		public int Count { get; set; }
	}
}
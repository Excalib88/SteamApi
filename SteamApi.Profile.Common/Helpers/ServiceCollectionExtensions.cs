using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SteamApi.Profile.Common.ContentItemDetails;

namespace SteamApi.Profile.Common.Helpers
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSteamApi(this IServiceCollection services, IConfiguration configuration)
		{
			return services
				.AddHttpClients()
				.AddDomainServices()
				;
		}

		private static IServiceCollection AddHttpClients(this IServiceCollection services)
		{
			services.AddHttpClient("SteamApi", c => c.BaseAddress = new Uri(UrlConstants.SteamPoweredUrl));
			services.AddHttpClient("SteamProfile", c => c.BaseAddress = new Uri(UrlConstants.SteamCommunityUrl));
			services.AddHttpClient("SteamContent", c => c.BaseAddress = new Uri(UrlConstants.ImageServiceUrl));

			return services;
		}

		private static IServiceCollection AddDomainServices(this IServiceCollection services)
		{
			return services
				.AddScoped<IContentItemDetailService, ContentItemDetailService>();
		}
	}
}
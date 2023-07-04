using Microsoft.Extensions.Configuration;

namespace UserHubAdminPortal.Helpers
{
	public static class AppSettingsReader
	{
		private static readonly IConfiguration Configuration;
		public static AppSettingsModel appSettings;

		static AppSettingsReader()
		{
			Configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();
		}

		public static void GetAppSettings()
		{
			appSettings = new();
			appSettings.UserHubApi = Configuration["HttpClient:userHubApi"];
			appSettings.ApiUrl = Configuration["HttpClient:apiUrl"];
		}
	}

	public class AppSettingsModel
	{
		public string ApiUrl { get; set; }
		public string UserHubApi { get; set; }
	}
}

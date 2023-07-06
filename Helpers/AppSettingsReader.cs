using Microsoft.Extensions.Configuration;

namespace UserHubAdminPortal.Helpers
{
	public static class AppSettingsReader
	{
		private static readonly IConfiguration Configuration;
		public static AppSettingsModel appSettings;

		static AppSettingsReader()
		{
            //Configuration = new ConfigurationBuilder()
            //	.AddJsonFile("appsettings.Development.json")
            //	.Build();
            
			var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
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

using Refit;
namespace HealthCareMobileApp.WebInterface
{
    sealed class WebClient
    {
        private readonly IHealthCareAPI api;
        private static readonly string BaseURL = "http://192.168.1.69";
        private static readonly WebClient Client = new WebClient();
        public static IHealthCareAPI WebAPI => Client.api;
        private WebClient()
        {
            api = RestService.For<IHealthCareAPI>(BaseURL);
        }
    }
}

using System;
using System.Globalization;
using Windows.Security.Authentication.Web;

namespace BDSA2017.Lecture11.App.Models
{
    public class Settings : ISettings
    {
        public string Tenant => "ondfisk.onmicrosoft.com";

        public string ClientId => "48ad45dd-7c9c-4e94-b2e2-c3b09aae44a3";

        public string RedirectUri => $"ms-appx-web://Microsoft.AAD.BrokerPlugIn/{WebAuthenticationBroker.GetCurrentApplicationCallbackUri().Host.ToUpper()}";

        public string Instance => "https://login.microsoftonline.com/";

        public string WebAccountProviderId => "https://login.microsoft.com";

        public string ApiResourceId => "https://ondfisk.onmicrosoft.com/BDSA2017.Lecture11.Web";

        public Uri ApiBaseAddress => new Uri("https://bdsa2017.azurewebsites.net/");

        public string Authority => $"{Instance}{Tenant}";
    }
}

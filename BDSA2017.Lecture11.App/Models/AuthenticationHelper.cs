using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.Storage;

namespace BDSA2017.Lecture11.App.Models
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly ISettings _settings;
        private readonly ApplicationDataContainer _appSettings;
        private WebAccountProvider _webAccountProvider;

        public AuthenticationHelper(ISettings settings)
        {
            _appSettings = ApplicationData.Current.RoamingSettings;
            _settings = settings;
        }

        public async Task<string> AcquireTokenAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<string> AcquireTokenSilentAsync()
        {
            if (_webAccountProvider == null)
            {
                _webAccountProvider = await WebAuthenticationCoreManager.FindAccountProviderAsync(_settings.WebAccountProviderId, _settings.Authority);
            }

            var userId = _appSettings.Values["userId"];
            var userAccount = await WebAuthenticationCoreManager.FindAccountAsync(_webAccountProvider, (string)userId);
            var webTokenRequest = new WebTokenRequest(_webAccountProvider, string.Empty, _settings.ClientId);
            webTokenRequest.Properties.Add("resource", _settings.ApiResourceId);
            var webTokenRequestResult = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(webTokenRequest, userAccount);
            if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
            {
                _appSettings.Values["userId"] = userAccount.Id;
                _appSettings.Values["login_hint"] = userAccount.UserName;
                return webTokenRequestResult.ResponseData[0].Token;
            }
            return null;
        }
    }
}

using BDSA2017.Lecture11.App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Core;
using BDSA2017.Lecture11.App.Models;
using System;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.Security.Authentication.Web.Core;
using Windows.UI.Popups;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BDSA2017.Lecture11.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ApplicationDataContainer _appSettings;
        private readonly ISettings _settings;
        private readonly MainPageViewModel _vm;

        private WebAccountProvider _webAccountProvider;
        private WebAccount _userAccount;

        public MainPage()
        {
            InitializeComponent();

            _appSettings = ApplicationData.Current.RoamingSettings;
            _settings = App.ServiceProvider.GetService<ISettings>();
            _vm = App.ServiceProvider.GetService<MainPageViewModel>();

            DataContext = _vm;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _webAccountProvider = await WebAuthenticationCoreManager.FindAccountProviderAsync(_settings.WebAccountProviderId, _settings.Authority);

            var webTokenRequest = new WebTokenRequest(_webAccountProvider, string.Empty, _settings.ClientId);
            webTokenRequest.Properties.Add("resource", _settings.ApiResourceId);

            // Check if there's a record of the last account used with the app
            var userId = _appSettings.Values["userId"];
            if (userId != null)
            {
                // Get an account object for the user
                _userAccount = await WebAuthenticationCoreManager.FindAccountAsync(_webAccountProvider, (string)userId);
                if (_userAccount != null)
                {
                    // Ensure that the saved account works for getting the token we need
                    var webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest, _userAccount);
                    if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
                    {
                        _userAccount = webTokenRequestResult.ResponseData[0].WebAccount;
                    }
                    else
                    {
                        // The saved account could not be used for getitng a token
                        var messageDialog = new MessageDialog("We tried to sign you in with the last account you used with this app, but it didn't work out. Please sign in as a different user.");
                        await messageDialog.ShowAsync();
                        // Make sure that the UX is ready for a new sign in
                        //UpdateUXonSignOut();
                        await SignInOut();
                    }
                }
                else
                {
                    // The WebAccount object is no longer available. Let's attempt a sign in with the saved username
                    webTokenRequest.Properties.Add("LoginHint", _appSettings.Values["login_hint"].ToString());
                    WebTokenRequestResult webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest);
                    if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
                    {
                        _userAccount = webTokenRequestResult.ResponseData[0].WebAccount;
                    }
                }
            }
            else
            {
                // There is no recorded user. Let's start a sign in flow without imposing a specific account.                             
                var webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest);
                if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
                {
                    _userAccount = webTokenRequestResult.ResponseData[0].WebAccount;
                }
            }

            if (_userAccount != null) // we succeeded in obtaining a valid user
            {
                // save user ID in local storage
                //UpdateUXonSignIn();
            }
            else
            {
                // nothing we tried worked. Ensure that the UX reflects that there is no user currently signed in.
                //UpdateUXonSignOut();
                //MessageDialog messageDialog = new MessageDialog("We could not sign you in. Please try again.");
                //await messageDialog.ShowAsync();
                await SignInOut();
            }
            _appSettings.Values["userId"] = _userAccount.Id;
            _appSettings.Values["login_hint"] = _userAccount.UserName;

            _vm.Initialize();

            var rootFrame = Window.Current.Content as Frame;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
        }

        private async Task SignInOut()
        {
            // prepare a request with 'WebTokenRequestPromptType.ForceAuthentication', 
            // which guarantees that the user will be able to enter an account of their choosing
            // regardless of what accounts are already present on the system
            var webTokenRequest = new WebTokenRequest(_webAccountProvider, string.Empty, _settings.ClientId, WebTokenRequestPromptType.ForceAuthentication);
            webTokenRequest.Properties.Add("resource", _settings.ApiResourceId);
            var webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest);
            if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
            {
                _userAccount = webTokenRequestResult.ResponseData[0].WebAccount;
                //UpdateUXonSignIn();
            }
            else
            {
                //UpdateUXonSignOut();
                MessageDialog messageDialog = new MessageDialog("We could not sign you in. Please try again.");
                await messageDialog.ShowAsync();
            }
        }

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(CharacterPage), e.AddedItems[0]);
        }
    }
}

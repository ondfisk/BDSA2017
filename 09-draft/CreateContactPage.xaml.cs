using BDSA2017.Lecture09.MVVM.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BDSA2017.Lecture09.MVVM.Views
{
    public sealed partial class CreateContactPage : Page
    {
        public CreateContactPage()
        {
            InitializeComponent();

            var vm = new ContactPageViewModel
            {
                BackCommand = new RelayCommand(o => { if (Frame.CanGoBack) { Frame.GoBack(); } })
            };

            DataContext = vm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
        }
    }
}

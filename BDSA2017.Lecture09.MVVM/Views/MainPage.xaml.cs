using BDSA2017.Lecture09.MVVM.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BDSA2017.Lecture09.MVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            var vm = new MainPageViewModel
            {
                GoToAlbumsPageCommand = new RelayCommand(o => Frame.Navigate(typeof(AlbumsPage))),
                GoToContactsPageCommand = new RelayCommand(o => Frame.Navigate(typeof(ContactsPage)))
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

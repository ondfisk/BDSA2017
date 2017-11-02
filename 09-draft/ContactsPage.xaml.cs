using BDSA2017.Lecture09.MVVM.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BDSA2017.Lecture09.MVVM.Views
{
    public sealed partial class ContactsPage : Page
    {
        private readonly ContactsPageViewModel _vm;

        public ContactsPage()
        {
            InitializeComponent();

            _vm = new ContactsPageViewModel
            {
                NewCommand = new RelayCommand(o => Frame.Navigate(typeof(CreateContactPage)))
            };

            DataContext = _vm;
        }

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = sender as GridView;

            _vm.Contacts.Remove(grid.SelectedItem as ContactPageViewModel);
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

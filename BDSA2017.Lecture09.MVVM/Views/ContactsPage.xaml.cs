using BDSA2017.Lecture09.MVVM.ViewModels;
using Windows.UI.Xaml.Controls;

namespace BDSA2017.Lecture09.MVVM.Views
{
    public sealed partial class ContactsPage : Page
    {
        private readonly ContactsPageViewModel _vm;

        public ContactsPage()
        {
            InitializeComponent();

            _vm = new ContactsPageViewModel();
            _vm.BackCommand = new RelayCommand(o => Frame.Navigate(typeof(MainPage)));
            _vm.NewCommand = new RelayCommand(o => Frame.Navigate(typeof(CreateContactPage)));
            _vm.Initialize();

            DataContext = _vm;
        }

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = sender as GridView;

            _vm.Contacts.Remove(grid.SelectedItem as ContactPageViewModel);
        }
    }
}

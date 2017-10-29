using BDSA2017.Lecture09.MVVM.ViewModels;
using Windows.UI.Xaml.Controls;

namespace BDSA2017.Lecture09.MVVM.Views
{
    public sealed partial class CreateContactPage : Page
    {
        public CreateContactPage()
        {
            InitializeComponent();

            var vm = new ContactPageViewModel();
            vm.BackCommand = new RelayCommand(o => Frame.Navigate(typeof(ContactsPage)));

            DataContext = vm;
        }
    }
}

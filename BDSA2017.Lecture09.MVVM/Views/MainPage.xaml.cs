using BDSA2017.Lecture09.MVVM.ViewModels;
using Windows.UI.Xaml.Controls;

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

            var vm = new MainPageViewModel();
            vm.GoToAlbumsPageCommand = new RelayCommand(o => Frame.Navigate(typeof(AlbumsPage)));
            vm.GoToContactsPageCommand = new RelayCommand(o => Frame.Navigate(typeof(ContactsPage)));

            DataContext = vm;
        }
    }
}

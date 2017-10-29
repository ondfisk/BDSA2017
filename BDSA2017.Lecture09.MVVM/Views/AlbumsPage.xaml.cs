using BDSA2017.Lecture09.MVVM.ViewModels;
using Windows.UI.Xaml.Controls;

namespace BDSA2017.Lecture09.MVVM.Views
{
    public sealed partial class AlbumsPage : Page
    {
        public AlbumsPage()
        {
            InitializeComponent();

            var vm = new AlbumViewModel();
            vm.BackCommand = new RelayCommand(o => Frame.Navigate(typeof(MainPage)));
            vm.Initialize();

            DataContext = vm;
        }
    }
}

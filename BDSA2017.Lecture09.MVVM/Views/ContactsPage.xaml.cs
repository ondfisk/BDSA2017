using BDSA2017.Lecture09.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BDSA2017.Lecture09.MVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

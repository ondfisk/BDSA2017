using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BDSA2017.Lecture11.App.Models
{
    public class NavigationService : INavigationService
    {
        public bool Navigate(Type sourcePageType, object parameter)
        {
            if (Window.Current.Content is Frame rootFrame)
            {
                return rootFrame.Navigate(sourcePageType, parameter);
            }

            return false;
        }
    }
}

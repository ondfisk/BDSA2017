using System;

namespace BDSA2017.Lecture11.App.Models
{
    public interface INavigationService
    {
        bool Navigate(Type sourcePageType, object parameter);
    }
}

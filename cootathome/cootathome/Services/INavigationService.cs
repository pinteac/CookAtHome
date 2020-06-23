using cootathome.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace cootathome.Services
{
    public interface INavigationService
    {
        Page MainPage { get; }

        void Configure(string key, Type pageType);

        void GoBack(object parameter = null);

        void GoToRootPage(object pameter = null);

        void NavigateTo(string pageKey, object parameter = null, INavigationService obj = null);
    }
}

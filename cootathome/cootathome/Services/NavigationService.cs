using cootathome.Utlity;
using cootathome.ViewModels;
using cootathome.Views;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace cootathome.Services
{
    public class NavigationService : INavigationService
    {
        // Hold all the pages 
        private Dictionary<string, Type> pages { get; } = new Dictionary<string, Type>();
        //Link to theh main page
        public Page MainPage => Application.Current.MainPage;
        //Add pages to dictionary
        public void Configure(string key, Type pageType)
        {
            pages[key] = pageType;
        }
        // Move to previous page
        public void GoBack(object parameter = null)
        {
            MessagingCenter.Send(this, MessageNames.CleanUp);
            MainPage.Navigation.PopAsync();
        }

        public void GoToRootPage(object parameter = null)
        {
            MainPage.Navigation.PopToRootAsync();
        }



        // Navigates to the next page by pushing the next page received
        public void NavigateTo(string pageKey, object parameter = null, INavigationService obj = null)
        {
            if(pages.TryGetValue(pageKey, out Type pageType))
            {
                var page = (Page)Activator.CreateInstance(pageType);
                page.SetNavigationArgs(parameter);

                MainPage.Navigation.PushAsync(page);

                (page.BindingContext as BaseViewModel).InitializeItem(parameter);
                (page.BindingContext as BaseViewModel).Initialize(parameter);
                (page.BindingContext as BaseViewModel).CleanUp(obj);
            }
            else
            {
                throw new ArgumentException($"This page doesn't exist: {pageKey}.");
            }
        }
    }

    public static class NavigationExtensions
    {
        private static ConditionalWeakTable<Page, object> arguments = new ConditionalWeakTable<Page, object>();

        public static object GetNavigationArgs(this Page page)
        {
            object argument;
            arguments.TryGetValue(page, out argument);

            return argument;
        }

        public static void SetNavigationArgs(this Page page, object args) => arguments.Add(page, args);
    }
}

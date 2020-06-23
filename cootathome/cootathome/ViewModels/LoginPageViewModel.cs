using cootathome.Models;
using cootathome.Services;
using cootathome.Utlity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace cootathome.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IUserDataService _userDataService;

        private User _userName;
        private string userName;
        private string password;
        private string errormsg;

        public ICommand LoginCommand { get; }

        public ICommand RegisterCommand { get; }

        public ICommand ChangePasswordCommand { get; }

        public LoginPageViewModel(INavigationService navigationService, IUserDataService userDataService)
        {
            _navigationService = navigationService;
            _userDataService = userDataService;

            _userName = new User();

            LoginCommand = new Command(OnLoginCommand);
            RegisterCommand = new Command(MoveToRegister);
            ChangePasswordCommand = new Command(MoveToChangePassword);

            MessagingCenter.Subscribe<AddNewReceipeViewModel>(this, MessageNames.GiveMeTheUserID, OnUserIDRequest);
            MessagingCenter.Subscribe<CategoryDetailViewModel>(this, MessageNames.GiveMeTheUserID, OnUserIDRequest);
            MessagingCenter.Subscribe<HomePageViewModel>(this, MessageNames.Logout, OnLogout);

            MessagingCenter.Subscribe<NavigationService>(this, MessageNames.CleanUp, CleanUp);
        }

        public override void CleanUp(INavigationService obj)
        {
            UserName = null;
            Password = null;
            ErrorMsg = null;
        }
        private void OnLogout(HomePageViewModel obj)
        {
            UserName = null;
            Password = null;
            _userName = null;
        }

        private void OnUserIDRequest(object obj)
        {
            MessagingCenter.Send(this, MessageNames.TakeTheUser, _userName);
        }

        private async void OnLoginCommand()
        {
            ErrorMsg = null;
            if (UserName == null)
                ErrorMsg = MessageNames.LoginBlank;
            try
            {
               _userName =  await _userDataService.GetAsyncTheUser(UserName);
                if (_userName.Password != Password)
                    ErrorMsg = MessageNames.LoginWrongPassword;
            }
            catch (InvalidOperationException)
            {
                ErrorMsg = MessageNames.LoginNotRegistered;
            }

            if (ErrorMsg == null)
            {
                _navigationService.NavigateTo(ViewNames.HomePageView, MessageNames.UserLoggedIn);
                //UserName = null;
                //Password = null;
                //ErrorMsg = null;
            }
        }

        private void MoveToRegister()
        {
            _navigationService.NavigateTo(ViewNames.RegisterPageView);
            //UserName = null;
            //Password = null;
            //ErrorMsg = null;
        }

        private void MoveToChangePassword()
        {
            _navigationService.NavigateTo(ViewNames.ChangePasswordView);
            //UserName = null;
            //Password = null;
            //ErrorMsg = null;
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public string ErrorMsg
        {
            get
            {
                return errormsg;
            }
            set
            {
                errormsg = value;
                OnPropertyChanged("ErrorMsg");
            }
        }
    }
}

using cootathome.Models;
using cootathome.Services;
using cootathome.Utlity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace cootathome.ViewModels
{
    public class ChangePasswordViewModel:BaseViewModel
    {
        private INavigationService _navigationService;
        private IUserDataService _userDataService;
     
        private User _userName;
        private string userName;
        private string password;
        private string confirmpassword;
        private string errormsg;

        public ICommand ReturnToLoginCommand { get; }

        public ICommand ChangePasswordCommand { get; }


        public ChangePasswordViewModel(INavigationService navigationService, IUserDataService userDataService)
        {
            _navigationService = navigationService;
            _userDataService = userDataService;

            _userName = new User();

            ReturnToLoginCommand = new Command(MoveBackToLoginCommand);
            ChangePasswordCommand = new Command(OnChangePasswordCommand);
        }

        //public override void InitializeItem(object parameter)
        //{
        //    MessagingCenter.Send(this, MessageNames.CleanUp);
        //}
        private async void OnChangePasswordCommand(object obj)
        {
            CPErrorMsg = null;
            if (CPUserName == null || CPPassword == null || CPConfirmPassword == null)
                CPErrorMsg = MessageNames.RegisterBlank;
            else
            {
                    CPErrorMsg = ValidationUtilities.ValidatePassword(CPPassword);
                    if (CPErrorMsg == null)
                    {
                        if (CPPassword != CPConfirmPassword)
                            CPErrorMsg = MessageNames.RegisterWrongPasswords;
                        else
                        {
                            try
                            {
                                _userName = await _userDataService.GetAsyncTheUser(CPUserName);
                                _userName.Password = CPPassword;
                                await _userDataService.RegisterUser(_userName);
                            }
                            catch (InvalidOperationException)
                            {
                                CPErrorMsg = MessageNames.RegisterUserException;
                            }
                            catch (Exception)
                            {
                                CPErrorMsg = MessageNames.RegisterException;
                            }
                        }
                    }

            }

            if (CPErrorMsg == null)
            {
                _navigationService.GoBack();
            }
        }

        private void MoveBackToLoginCommand()
        {
            _navigationService.GoBack();
        }

        public string CPUserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("CPUserName");
            }
        }

        public string CPPassword
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("CPPassword");
            }
        }

        public string CPConfirmPassword
        {
            get
            {
                return confirmpassword;
            }
            set
            {
                confirmpassword = value;
                OnPropertyChanged("CPConfirmPassword");
            }
        }

        public string CPErrorMsg
        {
            get
            {
                return errormsg;
            }
            set
            {
                errormsg = value;
                OnPropertyChanged("CPErrorMsg");
            }
        }
    }
}

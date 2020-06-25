using cootathome.Models;
using cootathome.Services;
using cootathome.Utlity;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace cootathome.ViewModels
{
    public class RegisterPageViewModel:BaseViewModel
    {
        private INavigationService _navigationService;
        private IUserDataService _userDataService;

        private User _userName;
        private string firstName;
        private string lastName;
        private string userName;
        private string password;
        private string confirmpassword;
        private string email;
        private string errormsg;

        public ICommand RegisterCommand { get; }

        public ICommand LoginCommand { get; }

        public RegisterPageViewModel(INavigationService navigationService, IUserDataService userDataService)
        {
            _navigationService = navigationService;
            _userDataService = userDataService;

            _userName = new User();

            RegisterCommand = new Command(OnRegisterCommand);
            LoginCommand = new Command(MoveToLoginPage);
        }

        public override void CleanUp(INavigationService obj)
        {
            RegisterErrorMsg = null;
        }

        private async void OnRegisterCommand()
        {
            RegisterErrorMsg = null;
            if (RegisterUserName == null || RegisterPassword == null || RegisterConfirmPassword == null || RegisterFirstName == null || RegisterLastName == null || RegisterEmail == null)
                RegisterErrorMsg = MessageNames.RegisterBlank;
            else
            {
                RegisterErrorMsg = ValidationUtilities.ValidateInput(RegisterFirstName);
                if (RegisterErrorMsg == null)
                {
                    RegisterErrorMsg = ValidationUtilities.ValidateInput(RegisterLastName);
                    if (RegisterErrorMsg == null)
                    {
                        RegisterErrorMsg = ValidationUtilities.ValidateInput(RegisterUserName);
                        if (RegisterErrorMsg == null)
                        {
                            if(RegexUtilities.IsValidEmail(RegisterEmail))
                            {
                                RegisterErrorMsg = ValidationUtilities.ValidatePassword(RegisterPassword);
                                if(RegisterErrorMsg == null)
                                {
                                    if (RegisterPassword != RegisterConfirmPassword)
                                    {
                                        RegisterErrorMsg = MessageNames.RegisterWrongPasswords;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            _userName.FirstName = RegisterFirstName;
                                            _userName.LastName = RegisterLastName;
                                            _userName.UserName = RegisterUserName;
                                            _userName.Password = RegisterPassword;
                                            _userName.Email = RegisterEmail;
                                            await _userDataService.RegisterUser(_userName);
                                        }
                                        catch (Exception)
                                        {
                                            RegisterErrorMsg = MessageNames.RegisterUniqueException;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                RegisterErrorMsg = MessageNames.RegisterWrongEmail;
                            }
                        }
                    }
                }
            }

            if (RegisterErrorMsg == null)
            {
                MessagingCenter.Send(this, MessageNames.RegisterdUser);
                _navigationService.GoBack();
            }
        }

        private void MoveToLoginPage()
        {
            _navigationService.GoBack();
        }

        public string RegisterUserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("RegisterUserName");
            }
        }

        public string RegisterPassword
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("RegisterPassword");
            }
        }

        public string RegisterConfirmPassword
        {
            get
            {
                return confirmpassword;
            }
            set
            {
                confirmpassword = value;
                OnPropertyChanged("RegisterConfirmPassword");
            }
        }

        public string RegisterFirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("RegisterFirstName");
            }
        }

        public string RegisterLastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged("RegisterLastName");
            }
        }

        public string RegisterEmail
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("RegisterEmail");
            }
        }

        public string RegisterErrorMsg
        {
            get
            {
                return errormsg;
            }
            set
            {
                errormsg = value;
                OnPropertyChanged("RegisterErrorMsg");
            }
        }

    }
}

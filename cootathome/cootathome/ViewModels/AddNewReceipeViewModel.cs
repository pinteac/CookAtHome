using cootathome.Models;
using cootathome.Services;
using cootathome.Utlity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Essentials;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace cootathome.ViewModels
{
    public class AddNewReceipeViewModel:BaseViewModel
    {

        INavigationService _navigationService;
        IRecipeDataService _recipeDataService;
        IDialogService _dialogService;

        private ObservableCollection<Categories> _selectedCategory;
        private Categories _myselectedcategory;
        private MediaFile _mediaFile;
        public User _userOnline;


        private string errorMsg;

        public Recipe _recipe;
        private string recipeName;
        private string recipeDescription;
        private string recipeImageURL;

        public ICommand AddRecipe { get; }

        public ICommand AddNewCategory { get; }

        public ICommand AddRecipeImageURL { get; }

        public AddNewReceipeViewModel(INavigationService navigationService, IRecipeDataService recipeDataService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _recipeDataService = recipeDataService;
            _dialogService = dialogService;

            _myselectedcategory = new Categories();
            _userOnline = new User();
            _recipe = new Recipe();

            AddRecipeImageURL = new Command(OnAddRecipeImage);
            AddRecipe = new Command(OnAddRecipe);
            AddNewCategory = new Command(GoToCategoryPage);

            MessagingCenter.Subscribe<LoginPageViewModel, User>(this, MessageNames.TakeTheUser, OnReceiveLoggedUser);
            MessagingCenter.Subscribe<HomePageViewModel, ObservableCollection<Categories>>(this, MessageNames.CategoryChangedMessage, OnUpdateCategoryList);
        }

        private async void OnAddRecipeImage(object obj)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await _dialogService.ShowDialog(MessageNames.PhotoNotSupported, "Failure", "Ok");
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile == null)
                return;
            else
            {
                recipeImageURL = _mediaFile.Path;
            }
        }

        private async void OnUpdateCategoryList(HomePageViewModel arg1, ObservableCollection<Categories> arg2)
        {
            selectedCategory = arg2;
            await _dialogService.ShowDialog(MessageNames.CategorySuccessfulAdd, "Success", "Ok");
        }

        private void GoToCategoryPage(object obj)
        {
            _navigationService.NavigateTo(ViewNames.AddCategoryView);
        }

        private void OnReceiveLoggedUser(LoginPageViewModel arg1, User arg2)
        {
            _userOnline = arg2;
        }

        private async void OnAddRecipe()
        {
            ANRErrorMsg = null;
            if (TheSelectedCategory != null)
            {
                if (AddRecipeName != null)
                {
                    try
                    {
                        _recipe.Name = AddRecipeName;
                        _recipe.ImageURL = recipeImageURL;
                        _recipe.Description = AddRecipeDescription;
                        _recipe.UserID = _userOnline.ID;
                        _recipe.CategoryID = TheSelectedCategory.ID;

                        await _recipeDataService.AddNewRecipe(_recipe);
                    }
                    catch (Exception)
                    {
                        ANRErrorMsg = MessageNames.RecipeException;
                    }
                }
                else
                    ANRErrorMsg = MessageNames.RecipeNameValidation;
            }
            else
                ANRErrorMsg = MessageNames.RecipeWithoutCategory;
            

            if (ANRErrorMsg == null)
            {
                AddRecipeName = null;
                AddRecipeDescription = null;
                recipeImageURL = null;
                TheSelectedCategory = null;
                MessagingCenter.Send(this, MessageNames.RecipeAdded);
                _navigationService.GoBack();
            }
        }

        public override void InitializeItem(object parameter)
        {
            if (parameter == null)
                selectedCategory = new ObservableCollection<Categories>();
            else
                selectedCategory = parameter as ObservableCollection<Categories>;

            MessagingCenter.Send(this, MessageNames.GiveMeTheUserID);
        }

        public ObservableCollection<Categories> selectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("selectedCategory");
            }
        }

        public string ANRErrorMsg
        {
            get
            {
                return errorMsg;
            }
            set
            {
                errorMsg = value;
                OnPropertyChanged("ANRErrorMsg");
            }
        }

        public string AddRecipeName
        {
            get
            {
                return recipeName;
            }
            set
            {
                recipeName = value;
                OnPropertyChanged("AddRecipeName");
            }
        }

        public string AddRecipeDescription
        {
            get
            {
                return recipeDescription;
            }
            set
            {
                recipeDescription = value;
                OnPropertyChanged("AddRecipeDescription");
            }
        }

        public Categories TheSelectedCategory
        {
            get
            {
                return _myselectedcategory;
            }
            set
            {
                _myselectedcategory = value;
                OnPropertyChanged("TheSelectedCategory");
            }
        }


    }
}

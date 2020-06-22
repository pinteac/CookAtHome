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

namespace cootathome.ViewModels
{
    public class CategoryDetailViewModel:BaseViewModel
    {

        INavigationService _navigationService;
        ICategoriesDataService _categoriesDataService;
        IRecipeDataService _recipeDataService;
        IDialogService _dialogService;
        
        private Categories _selectedCategory;
        private string categoryName;

        private ObservableCollection<Recipe> _recipes;
        public User _userOnline;

        public ICommand RecipeSelectedCommand { get; }

        public ICommand DeleteCategory { get; }


        public CategoryDetailViewModel (INavigationService navigationService, ICategoriesDataService categoriesDataService, IRecipeDataService recipeDataService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _categoriesDataService = categoriesDataService;
            _recipeDataService = recipeDataService;
            _dialogService = dialogService;

            RecipeSelectedCommand = new Command<Recipe>(OnRecipeSelectedCommand);
            DeleteCategory = new Command(OnDeleteCategory);

            MessagingCenter.Subscribe<RecipeDetailViewModel>(this, MessageNames.RecipeDeleted, UpdateRecipies);
            MessagingCenter.Subscribe<LoginPageViewModel, User>(this, MessageNames.TakeTheUser, OnReceiveLoggedUser);
        }

        private void OnReceiveLoggedUser(LoginPageViewModel arg1, User arg2)
        {
            _userOnline = arg2;
        }

        private async void OnDeleteCategory()
        {
            _recipeDataService.DeleteAsyncRecipeByCategory(_selectedCategory.ID);
            await _categoriesDataService.DeleteAsyncCategory(_selectedCategory);
            MessagingCenter.Send(this, MessageNames.CategoryDeleted);
            _navigationService.GoBack();
        }

        private async void UpdateRecipies(object obj)
        {
            await Initialize(obj);
            await _dialogService.ShowDialog(MessageNames.RecipeSuccessfullyDeleted, "Success", "Ok");
        }

        private void OnRecipeSelectedCommand(Recipe obj)
        {
            _navigationService.NavigateTo(ViewNames.RecipeDetailView, obj);
        }

        public override async Task Initialize(object obj)
        {
            
            Recipes = (await _recipeDataService.GetAllRecipesWithID(_selectedCategory.ID, _userOnline.ID)).ToObservableCollection();
        }

        public override void InitializeItem(object parameter)
        {
            if (parameter != null)
            {
                _selectedCategory = (Categories)parameter;
                SelectedCategoryName = _selectedCategory.CategoryName;
            }
            MessagingCenter.Send(this, MessageNames.GiveMeTheUserID);
        }

        public ObservableCollection<Recipe> Recipes
        {
            get
            {
                return _recipes;
            }
            set
            {
                _recipes = value;
                RaisePropertyChanged();
            }
        }
        public Categories SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public string SelectedCategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                OnPropertyChanged("SelectedCategoryName");
            }
        }

    }
}

using cootathome.Models;
using cootathome.Services;
using cootathome.Utlity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace cootathome.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {

        INavigationService _navigationService;
        ICategoriesDataService _categoriesDataService;
        IDialogService _dialogService;
        IRecipeDataService _recipeDataService;

        private ObservableCollection<Categories> _categories;
        private ObservableCollection<Recipe> _recipes;

        private Command _searchCommand;
        private bool _isCategoryListVisible;
        private bool _isRecipeListVisible;
        private string searchedText = string.Empty;

        public ICommand CategorySelectedCommand { get; }

        public ICommand GoToReceipe { get; }

        public ICommand RecipeSelectedCommand { get; }

        public ICommand Logout { get; }

        public ICommand SearchCommand
        {
            get
            {
                _searchCommand = _searchCommand ?? new Command(DoSearchCommand, CanExecuteSearchCommand);
                return _searchCommand;
            }
        }
        private void DoSearchCommand()
        {
            OnPropertyChanged("Recipes");
        }
        private bool CanExecuteSearchCommand()
        {
            return true;
        }

        public  HomePageViewModel(INavigationService navigationService, ICategoriesDataService categoriesDataService, IDialogService dialogService, IRecipeDataService recipeDataService)
        {
            _navigationService = navigationService;
            _categoriesDataService = categoriesDataService;
            _dialogService = dialogService;
            _recipeDataService = recipeDataService;

            _categories = new ObservableCollection<Categories>();
            _recipes = new ObservableCollection<Recipe>();
            isCategoryListVisible = true;
            isRecipeListVisible = false;

            CategorySelectedCommand = new Command<Categories>(OnCategorySelectedCommand);
            GoToReceipe = new Command<ObservableCollection<Categories>>(GotoRecipe);
            RecipeSelectedCommand = new Command<Recipe>(OnRecipeSelectedCommand);
            Logout = new Command(OnLogout);

            MessagingCenter.Subscribe<AddCategoryViewModel>(this, MessageNames.CategoryChangedMessage, OnCategoryChanges);
            MessagingCenter.Subscribe<CategoryDetailViewModel>(this, MessageNames.CategoryChangedMessage, OnCategoryUpdate);
            MessagingCenter.Subscribe<AddNewReceipeViewModel>(this, MessageNames.RecipeAdded, UpdateMessageAboutRecipe);
            MessagingCenter.Subscribe<CategoryDetailViewModel>(this, MessageNames.CategoryDeleted, UpdateMessageAboutCategory);
        }

        private async void OnCategoryUpdate(CategoryDetailViewModel obj)
        {
            Categories = (await _categoriesDataService.GetAllCategories()).ToObservableCollection();
        }

        private void OnLogout(object obj)
        {
            MessagingCenter.Send(this, MessageNames.Logout);
            _navigationService.GoToRootPage();
        }

        private void OnRecipeSelectedCommand(Recipe obj)
        {
            _navigationService.NavigateTo(ViewNames.RecipeDetailView, obj);
            //SearchedText = string.Empty;
        }

        private async void UpdateMessageAboutCategory(object obj)
        {
            Categories = (await _categoriesDataService.GetAllCategories()).ToObservableCollection();
            await _dialogService.ShowDialog(MessageNames.CategorySuccessfullyDeleted, "Success", "Ok");
        }

        private async void UpdateMessageAboutRecipe(AddNewReceipeViewModel obj)
        {
            Recipes = (await _recipeDataService.GetAllRecipes()).ToObservableCollection();
            await _dialogService.ShowDialog(MessageNames.RecipeSuccsessfulMessage, "Success", "Ok");
        }

        private void GotoRecipe(ObservableCollection<Categories> mycategories)
        {
            mycategories = _categories;
            _navigationService.NavigateTo(ViewNames.AddNewReceipeView, mycategories);
        }

        private async void OnCategoryChanges(object obj)
        {
            Categories = (await _categoriesDataService.GetAllCategories()).ToObservableCollection();
            MessagingCenter.Send(this, MessageNames.CategoryChangedMessage, _categories);
        }

        public override async Task Initialize(object data)
        {
            Categories = (await _categoriesDataService.GetAllCategories()).ToObservableCollection();
            Recipes = (await _recipeDataService.GetAllRecipes()).ToObservableCollection();
        }

        private void OnCategorySelectedCommand(Categories selectedCategory)
        {
            _navigationService.NavigateTo(ViewNames.CategoryDetailView, selectedCategory);
            selectedCategory = null;
        }

        public string SearchedText
        {
            get { return searchedText; }
            set
            {
                if (searchedText != value)
                {
                    searchedText = value ?? null;
                    OnPropertyChanged("SearchedText");

                    if (SearchCommand.CanExecute(null) && searchedText != string.Empty)
                    {
                        isCategoryListVisible = false;
                        isRecipeListVisible = true;
                        SearchCommand.Execute(null);
                    }
                    else
                    {
                        isRecipeListVisible = false;
                        isCategoryListVisible = true;
                    }

                }
            }
        }

        public ObservableCollection<Recipe> Recipes
        {
            get
            {
                if (searchedText != string.Empty)
                {
                    ObservableCollection<Recipe> theCollection = new ObservableCollection<Recipe>();

                    if (_recipes != null)
                    {
                        List<Recipe> entities = (from e in _recipes
                                                 where e.Name.Contains(searchedText)
                                                 select e).ToList<Recipe>();
                        if (entities != null && entities.Any())
                        {
                            theCollection = new ObservableCollection<Recipe>(entities);
                        }
                    }

                    return theCollection;
                }
                else
                    return _recipes;
            }
            set
            {
                _recipes = value;
            }
        }

        public ObservableCollection<Categories> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                RaisePropertyChanged();
            }
        }



        public bool isCategoryListVisible
        {
            get
            {
                return _isCategoryListVisible;
            }
            set
            {
                _isCategoryListVisible = value;
                OnPropertyChanged("isCategoryListVisible");
            }
        }

        public bool isRecipeListVisible
        {
            get
            {
                return _isRecipeListVisible;
            }
            set
            {
                _isRecipeListVisible = value;
                OnPropertyChanged("isRecipeListVisible");
            }
        }

        
    }
}

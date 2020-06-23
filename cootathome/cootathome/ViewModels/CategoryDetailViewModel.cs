using cootathome.Models;
using cootathome.Services;
using cootathome.Utlity;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
        private string imageURL;
        private const string _hardcodedImage = "Plate.jpg";
        private bool _isCategoryReadOnly;
        private bool _isEditVisible;
        private bool _isRecipeVisible;
        private bool _isDeleteEnabled;
        private bool _isEditButtonVisible;
        private bool _isSaveButtonVisible;
        private MediaFile _mediaFile;

        private ObservableCollection<Recipe> _recipes;
        public User _userOnline;

        public ICommand RecipeSelectedCommand { get; }

        public ICommand DeleteCategory { get; }

        public ICommand EditCategory { get; }

        public ICommand SaveCategory { get; }

        public ICommand AddCategoryImageURL { get; }

        public CategoryDetailViewModel (INavigationService navigationService, ICategoriesDataService categoriesDataService, IRecipeDataService recipeDataService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _categoriesDataService = categoriesDataService;
            _recipeDataService = recipeDataService;
            _dialogService = dialogService;

            RecipeSelectedCommand = new Command<Recipe>(OnRecipeSelectedCommand);
            DeleteCategory = new Command(OnDeleteCategory);
            EditCategory = new Command(OnEditCategory);
            SaveCategory = new Command(OnSaveCategory);
            AddCategoryImageURL = new Command(OnAddCategoryImageURL);

            MessagingCenter.Subscribe<RecipeDetailViewModel>(this, MessageNames.RecipeDeleted, UpdateRecipies);
            MessagingCenter.Subscribe<LoginPageViewModel, User>(this, MessageNames.TakeTheUser, OnReceiveLoggedUser);

            InitialStateOfPage();
        }

        private void InitialStateOfPage()
        {
            isCategoryReadOnly = true;
            isDeleteEnabled = true;
            isEditVisible = false;
            isRecipeVisible = true;
            isEditButtonVisible = true;
            isSaveButtonVisible = false;
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

        private void OnEditCategory()
        {
            isCategoryReadOnly = false;
            isDeleteEnabled = false;
            isEditVisible = true;
            isRecipeVisible = false;
            isEditButtonVisible = false;
            isSaveButtonVisible = true;
        }

        private async void OnSaveCategory()
        {
            bool isException = false;
            if (SelectedCategoryName != string.Empty)
            {
                if (SelectedCategory.CategoryName != SelectedCategoryName || SelectedCategory.ImageURL != SelectedCategoryImageURL)
                {
                    SelectedCategory.CategoryName = SelectedCategoryName;
                    SelectedCategory.ImageURL = SelectedCategoryImageURL;
                    try
                    {
                        await _categoriesDataService.AddNewCategory(SelectedCategory);
                    }
                    catch (Exception)
                    {
                        isException = true;
                        await _dialogService.ShowDialog(MessageNames.CategoryEditFail, "Failure", "Ok");
                    }
                    finally
                    {
                        InitialStateOfPage();

                        if (!isException)
                            await _dialogService.ShowDialog(MessageNames.CategorySuccessUpdate, "Success", "Ok");
                    }
                }
                else
                    InitialStateOfPage();
            }
            else
                await _dialogService.ShowDialog(MessageNames.CategoryNameValidation, "Failure", "Ok");
        }

        private async void OnAddCategoryImageURL()
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
                SelectedCategoryImageURL = _mediaFile.Path;
            }
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
                SelectedCategoryImageURL = _selectedCategory.ImageURL;
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

        public string SelectedCategoryImageURL
        {
            get
            {
                return imageURL;
            }
            set
            {
                imageURL = value;
                OnPropertyChanged("SelectedCategoryImageURL");
            }
        }

        public bool isCategoryReadOnly
        {
            get
            {
                return _isCategoryReadOnly;
            }
            set
            {
                _isCategoryReadOnly = value;
                OnPropertyChanged("isCategoryReadOnly");
            }
        }

        public bool isDeleteEnabled
        {
            get
            {
                return _isDeleteEnabled;
            }
            set
            {
                _isDeleteEnabled = value;
                OnPropertyChanged("isDeleteEnabled");
            }
        }
        public bool isEditVisible
        {
            get
            {
                return _isEditVisible;
            }
            set
            {
                _isEditVisible = value;
                OnPropertyChanged("isEditVisible");
            }
        }
        public bool isRecipeVisible
        {
            get
            {
                return _isRecipeVisible;
            }
            set
            {
                _isRecipeVisible = value;
                OnPropertyChanged("isRecipeVisible");
            }
        }
        public bool isEditButtonVisible
        {
            get
            {
                return _isEditButtonVisible;
            }
            set
            {
                _isEditButtonVisible = value;
                OnPropertyChanged("isEditButtonVisible");
            }
        }

        public bool isSaveButtonVisible
        {
            get
            {
                return _isSaveButtonVisible;
            }
            set
            {
                _isSaveButtonVisible = value;
                OnPropertyChanged("isSaveButtonVisible");
            }
        }


        

            
    }
}

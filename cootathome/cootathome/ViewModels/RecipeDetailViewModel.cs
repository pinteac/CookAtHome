using cootathome.Models;
using cootathome.Services;
using cootathome.Utlity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;
using System.Runtime.CompilerServices;

namespace cootathome.ViewModels
{
    public class RecipeDetailViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        IRecipeDataService _recipeDataService;
        IDialogService _dialogService;

        private MediaFile _mediaFile;

        private Recipe _selectedRecipe;
        private string recipeName;
        private string recipeImage;
        private string recipeDescription;

        private bool _isReadOnly;
        private bool _visibleEditButton;
        private bool _visibleSaveButton;
        private bool _visibleUploadButton;
        private bool _visibleReadDescription;
        private bool _visibleMessage;
        private string _readText;

        public ICommand DeleteRecipe { get; }

        public ICommand EditRecipe { get; }

        public ICommand SaveRecipe { get; }

        public ICommand UploadNewImage { get; }

        public ICommand ReadDescription { get;}


        public RecipeDetailViewModel(INavigationService navigationService, IRecipeDataService recipeDataService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _recipeDataService = recipeDataService;
            _dialogService = dialogService;

            DeleteRecipe = new Command<Recipe>(OnDeleteRecipe);
            EditRecipe = new Command(OnEditRecipe);
            SaveRecipe = new Command(OnSaveRecipe);
            UploadNewImage = new Command(OnUploadImage);
            ReadDescription = new Command(OnReadDescription);

            IsEditable = true;
            IsEditVisible = true;
            IsReadDescriptionVisible = true;
            IsSaveVisible = false;
            IsUploadVisible = false;
            isMessageVisible = false;
        }

        private async void OnReadDescription(object obj)
        {
            try
            {
                var locales = await TextToSpeech.GetLocalesAsync();
                Locale locale = (Locale)locales.ElementAt(50);
                if (SelectedRecipeDescription != string.Empty)
                {
                    await TextToSpeech.SpeakAsync(SelectedRecipeDescription, new SpeechOptions()
                    {
                        Locale = locale
                    });
                }
            }
            catch(Exception)
            {
                isMessageVisible = true;
                CannotReadText = MessageNames.CannotReadDescription;
            }
        }

        private async void OnUploadImage(object obj)
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
                SelectedRecipeImage = _mediaFile.Path;
            }
        }

        private async void OnSaveRecipe(object obj)
        {
            _selectedRecipe.Name = SelectedRecipeName;
            _selectedRecipe.Description = SelectedRecipeDescription;
            _selectedRecipe.ImageURL = SelectedRecipeImage;
            await _recipeDataService.AddNewRecipe(_selectedRecipe);
            
            IsEditable = true;
            IsEditVisible = true;
            IsReadDescriptionVisible = true;
            IsSaveVisible = false;
            IsUploadVisible = false;
        }

        private void OnEditRecipe(object obj)
        {
            IsEditable = false;
            IsReadDescriptionVisible = false;
            IsEditVisible = false;
            IsSaveVisible = true;
            IsUploadVisible = true;
        }

        private async void OnDeleteRecipe(Recipe recipe)
        {
            recipe = _selectedRecipe;
            await _recipeDataService.DeleteAsyncRecipe(recipe);
            MessagingCenter.Send(this, MessageNames.RecipeDeleted);
            _navigationService.GoBack();
        }

        public override void InitializeItem(object parameter)
        {
            if (parameter != null)
            {
                _selectedRecipe = (Recipe)parameter;
                SelectedRecipeName = _selectedRecipe.Name;
                SelectedRecipeImage = _selectedRecipe.ImageURL;
                SelectedRecipeDescription = _selectedRecipe.Description;
                
            }
        }

        public override Task Initialize(object parameter)
        {
            return base.Initialize(parameter);
        }

        public string SelectedRecipeName
        {
            get
            {
                return recipeName;
            }
            set
            {
                recipeName = value;
                OnPropertyChanged("SelectedRecipeName");
            }
        }

        public string SelectedRecipeImage
        {
            get
            {
                return recipeImage;
            }
            set
            {
                recipeImage = value;
                OnPropertyChanged("SelectedRecipeImage");
            }
        }

        public string SelectedRecipeDescription
        {
            get
            {
                return recipeDescription;
            }
            set
            {
                recipeDescription = value;
                OnPropertyChanged("SelectedRecipeDescription");
            }
        }

        public bool IsEditable
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged("IsEditable");
            }
        }

        public bool IsEditVisible
        {
            get
            {
                return _visibleEditButton;
            }
            set
            {
                _visibleEditButton = value;
                OnPropertyChanged("IsEditVisible");
            }
        }

        public bool IsSaveVisible
        {
            get
            {
                return _visibleSaveButton;
            }
            set
            {
                _visibleSaveButton = value;
                OnPropertyChanged("IsSaveVisible");
            }
        }

        public bool IsUploadVisible
        {
            get
            {
                return _visibleUploadButton;
            }
            set
            {
                _visibleUploadButton = value;
                OnPropertyChanged("IsUploadVisible");
            }
        }

        public bool IsReadDescriptionVisible
        {
            get
            {
                return _visibleReadDescription;
            }
            set
            {
                _visibleReadDescription = value;
                OnPropertyChanged("IsReadDescriptionVisible");
            }
        }

        public string CannotReadText
        {
            get
            {
                return _readText;
            }
            set
            {
                _readText = value;
                OnPropertyChanged("CannotReadText");
            }
        }

        public bool isMessageVisible
        {
            get
            {
                return _visibleMessage;
            }
            set
            {
                _visibleMessage = value;
                OnPropertyChanged("isMessageVisible");
            }
        }
        

    }
}

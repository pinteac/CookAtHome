using cootathome.Models;
using cootathome.Services;
using cootathome.Utlity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace cootathome.ViewModels
{
    public class AddCategoryViewModel:BaseViewModel
    {
        INavigationService _navigationService;
        ICategoriesDataService _categoriesDataService;
        IDialogService _dialogService;
        
        public Categories _category;
        private string categoryName;
        private string imageURL;
        private const string _hardcodedImage = "Plate.jpg";

        private MediaFile _mediaFile;
        private string errorMsg;

        public ICommand AddNewCategory { get; }
        public ICommand AddCategoryImageURL { get; }

        public AddCategoryViewModel(INavigationService navigationService, ICategoriesDataService categoriesDataService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _categoriesDataService = categoriesDataService;
            _dialogService = dialogService;

            AddNewCategory = new Command(OnAddNewCategoryAsync);
            AddCategoryImageURL = new Command(OnAddCategoryImageURL);

            _category = new Categories();
        }

        private async void OnAddCategoryImageURL(object obj)
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
                imageURL = _mediaFile.Path;
            }
        }

        public async void OnAddNewCategoryAsync()
        {
            ACErrorMsg = null;
            if (categoryName != null)
            {
                if (imageURL == null)
                {
                    imageURL = _hardcodedImage;
                }

                try
                {
                    _category.CategoryName = AddCategoryName;
                    _category.ImageURL = imageURL;
                    await _categoriesDataService.AddNewCategory(_category);
                    MessagingCenter.Send(this, MessageNames.CategoryChangedMessage);
                    _category.ID = 0;
                    _category.CategoryName = string.Empty;
                }
                catch (Exception)
                {
                    ACErrorMsg = MessageNames.CategoryException;
                }
            }
            else
            {
                ACErrorMsg = MessageNames.CategoryNameValidation;
            }

            if (errorMsg == null)
            {
                AddCategoryName = null;
                imageURL = null;
                _navigationService.GoBack();
            }
        }

        public string AddCategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                OnPropertyChanged("AddCategoryName");
            }
        }


        public string ACErrorMsg
        {
            get
            {
                return errorMsg;
            }
            set
            {
                errorMsg = value;
                OnPropertyChanged("ACErrorMsg");
            }
        }
    }
}

using cootathome.Utlity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cootathome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetailView : ContentPage
    {
        public CategoryDetailView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.categoryDetailViewModel;
            OnBackButtonPressed();
        }
    }
}
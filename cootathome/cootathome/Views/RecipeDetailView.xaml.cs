using cootathome.Utlity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cootathome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailView : ContentPage
    {
        public RecipeDetailView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.recipeDetailViewModel;
        }
    }
}
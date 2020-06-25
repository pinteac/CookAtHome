using cootathome.Utlity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cootathome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCategoryView : ContentPage
    {
        public AddCategoryView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.addCategoryViewModel;
        }
    }
}
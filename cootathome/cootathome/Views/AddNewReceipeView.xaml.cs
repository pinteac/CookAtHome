using cootathome.Utlity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cootathome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewReceipeView : ContentPage
    {
        public AddNewReceipeView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.addNewReceipeViewModel;
        }
    }
}
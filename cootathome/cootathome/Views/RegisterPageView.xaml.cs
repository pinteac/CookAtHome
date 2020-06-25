using cootathome.Utlity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cootathome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPageView : ContentPage
    {
        public RegisterPageView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.registerPageViewModel;
        }
    }
}
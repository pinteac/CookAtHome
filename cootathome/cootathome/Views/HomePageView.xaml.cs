using cootathome.Utlity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cootathome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageView : ContentPage
    {
        public HomePageView()
        {

            InitializeComponent();
            BindingContext = ViewModelLocator.homePageViewModel;
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}
using cootathome.Utlity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cootathome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordView : ContentPage
    {
        public ChangePasswordView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.changePasswordViewModel;
        }
    }
}
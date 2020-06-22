using cootathome.Utlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
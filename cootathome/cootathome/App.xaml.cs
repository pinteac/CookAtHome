using cootathome.Data;
using cootathome.Services;
using cootathome.Utlity;
using cootathome.Views;
using System;
using System.IO;
using Xamarin.Forms;

namespace cootathome
{
    public partial class App : Application
    {
        static Database database;

        public static NavigationService NavigationService { get; } = new NavigationService();
        public static UserDataService UserDataService { get; } = new UserDataService();
        public static CategoriesDataService CategoriesDataService { get; } = new CategoriesDataService();
        public static RecipeDataService RecipeDataService { get; } = new RecipeDataService();
        public static DialogService DialogService { get; } = new DialogService();

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                   Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                   LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            NavigationService.Configure(ViewNames.LoginPageView, typeof(LoginPageView));
            NavigationService.Configure(ViewNames.RegisterPageView, typeof(RegisterPageView));
            NavigationService.Configure(ViewNames.ChangePasswordView, typeof(ChangePasswordView));
            NavigationService.Configure(ViewNames.HomePageView, typeof(HomePageView));
            NavigationService.Configure(ViewNames.AddNewReceipeView, typeof(AddNewReceipeView));
            NavigationService.Configure(ViewNames.AddCategoryView, typeof(AddCategoryView));
            NavigationService.Configure(ViewNames.CategoryDetailView, typeof(CategoryDetailView));
            NavigationService.Configure(ViewNames.RecipeDetailView, typeof(RecipeDetailView));
            MainPage = new NavigationPage(new LoginPageView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

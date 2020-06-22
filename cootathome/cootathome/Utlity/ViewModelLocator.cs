using cootathome.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace cootathome.Utlity
{
    public static class ViewModelLocator
    {
        public static LoginPageViewModel loginPageViewModel { get; set; } = new LoginPageViewModel(App.NavigationService, App.UserDataService);

        public static RegisterPageViewModel registerPageViewModel { get; set; } = new RegisterPageViewModel(App.NavigationService, App.UserDataService);

        public static ChangePasswordViewModel changePasswordViewModel { get; set; } = new ChangePasswordViewModel(App.NavigationService, App.UserDataService);

        public static HomePageViewModel homePageViewModel { get; set; } = new HomePageViewModel(App.NavigationService, App.CategoriesDataService, App.DialogService, App.RecipeDataService);

        public static AddNewReceipeViewModel addNewReceipeViewModel { get; set; } = new AddNewReceipeViewModel(App.NavigationService, App.RecipeDataService, App.DialogService);

        public static AddCategoryViewModel addCategoryViewModel{ get; set; } = new AddCategoryViewModel(App.NavigationService, App.CategoriesDataService, App.DialogService);

        public static CategoryDetailViewModel categoryDetailViewModel { get; set; } = new CategoryDetailViewModel(App.NavigationService, App.CategoriesDataService, App.RecipeDataService, App.DialogService);

        public static RecipeDetailViewModel recipeDetailViewModel { get; set; } = new RecipeDetailViewModel(App.NavigationService, App.RecipeDataService, App.DialogService);
    }
}

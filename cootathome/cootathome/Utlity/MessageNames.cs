using System;
using System.Collections.Generic;
using System.Text;

namespace cootathome.Utlity
{
    public class MessageNames
    {
        public const string CategoryChangedMessage = "CategoryChanged";
        public const string GiveMeTheUserID = "GiveMeTheUserID";
        public const string TakeTheUser = "TakeTheUser";
        public const string UserLoggedIn = "UserLoggedIn";
        public const string RecipeDeleted = "RecipeDeleted";
        public const string RecipeAdded = "RecipeAdded";
        public const string CategoryDeleted = "CategoryDeleted";
        public const string Logout = "Logout";
        public const string CleanUp = "CleanUp";

        // User
        public const string LoginBlank = "Enter your login credentials!";
        public const string LoginWrongPassword = "Password is incorrect!";
        public const string LoginNotRegistered = "Please register before trying to sign in!";
        public const string RegisterBlank = "Please fill in all the fields!";
        public const string RegisterWrongPasswords = "Passwords do not match! Please re-type your password!";
        public const string RegisterUserException = "Please add a correct user name!";
        public const string RegisterException = "Please try again!";
        public const string RegisterUniqueException = "Username or email already existing! Please enter diferent credentials!";
        public const string RegisterWrongEmail = "Invalid email! Please re-type another email!";

        public const string CannotReadDescription = "Cannot read the description, because the device does not support this functionality!";
        public const string PhotoNotSupported = "Photo Upload not supported!";

        public const string RecipeSuccsessfulMessage = "Recipe was added successfully!";
        public const string RecipeSuccessfullyDeleted = "Recipe successfully deleted!";

        // Category
        public const string CategoryException = "Category not added! Please try again!";
        public const string CategoryNameValidation = "Please add a category name!";
        public const string CategorySuccessfulAdd = "The new category has been successfully added!";
        public const string CategorySuccessfullyDeleted = "Category has been deleted!";
        public const string CategoryUpdateAll = "CategoryUpdateAll";
        public const string CategoryEditFail = "The category has not been updated! Please try again!";
        public const string CategorySuccessUpdate = "The category has been successfully updated!";

        // Recipe
        public const string RecipeException = "Recipe not added! Please try again!";
        public const string RecipeNameValidation = "Please add a recipe name!";
        public const string RecipeWithoutCategory = "Please select a category for your recipe!";
        public const string RecipeUpdate = "Recipe has been successfully updated!";
    }
}

using cootathome.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cootathome.Services
{
    public class RecipeDataService : IRecipeDataService
    {
        public async Task<int> AddNewRecipe(Recipe newRecipe)
        {
            return await App.Database.SaveRecipeAsync(newRecipe);
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await App.Database.GetAllRecipes();
        }

        public async Task<List<Recipe>> GetAllRecipesWithID(int CategoryID, int UserID)
        {
            return await App.Database.GetAllRecipesbyId(CategoryID,UserID);
        }

        public async Task<Recipe> GetAsyncRecipe(string recipe)
        {
            return await App.Database.GetOneRecipe(recipe);
        }

        public async Task<int> DeleteAsyncRecipe(Recipe recipe)
        {
            return await App.Database.DeleteRecipeAsync(recipe);
        }

        public void DeleteAsyncRecipeByCategory(int categoryID)
        {
            App.Database.DeleteRecipesAsync(categoryID);
        }
    }
}

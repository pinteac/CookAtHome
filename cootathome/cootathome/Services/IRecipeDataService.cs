using cootathome.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace cootathome.Services
{
    public interface IRecipeDataService
    {

        Task<int> AddNewRecipe(Recipe newRecipe);

        Task<List<Recipe>> GetAllRecipes();

        Task<List<Recipe>> GetAllRecipesWithID(int CategoryID, int UserID);

        Task<List<Recipe>> GetAllRecipesOfUser(int UserID);

        Task<Recipe> GetAsyncRecipe(string recipe);

        Task<int> DeleteAsyncRecipe(Recipe recipe);

        void DeleteAsyncRecipeByCategory(int categoryID);
    }
}

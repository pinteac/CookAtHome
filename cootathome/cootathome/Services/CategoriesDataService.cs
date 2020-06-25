using cootathome.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cootathome.Services
{
    public class CategoriesDataService : ICategoriesDataService
    {
        public async Task<int> AddNewCategory(Categories newCategory)
        {
            return await App.Database.SaveCategoryAsync(newCategory);
        }

        public async Task<int> DeleteAsyncCategory(Categories categories)
        {
            return await App.Database.DeleteCategoryAsync(categories);
        }

        public async Task<List<Categories>> GetAllCategories()
        {
            return await App.Database.GetAllCategory();
        }

        public async Task<Categories> GetAsyncCategory(string category)
        {
            return await App.Database.GetOneCategory(category);
        }
    }
}

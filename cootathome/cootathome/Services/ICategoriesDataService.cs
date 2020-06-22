using cootathome.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cootathome.Services
{
    public interface ICategoriesDataService
    {

        Task<int> AddNewCategory(Categories newCategory);

        Task<List<Categories>> GetAllCategories();

        Task<Categories> GetAsyncCategory(string category);

        Task<int> DeleteAsyncCategory(Categories categories);

    }
}

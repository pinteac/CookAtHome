using cootathome.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cootathome.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Categories>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();
        }


        // USERS
        public Task<List<User>> GetAllUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }


        public Task<User> GetUserByUserNameAsync(string username)
        {
                return _database.Table<User>()
                .Where(i => i.UserName == username || i.Email == username)
                .FirstAsync();
        }


        public Task<int> SaveUserAsync(User user)
        {
            if (user.ID != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }


        // CATEGORIES
        public Task<Categories> GetOneCategory(string categoryName)
        {
            return _database.Table<Categories>()
                    .Where(i => i.CategoryName == categoryName)
                    .FirstAsync();
        }

        public Task<List<Categories>> GetAllCategory()
        {
            return _database.QueryAsync<Categories>("SELECT * FROM Categories");
        }

        public Task<int> SaveCategoryAsync(Categories newCategory)
        {
            if (newCategory.ID != 0)
            {
                return _database.UpdateAsync(newCategory);
            }
            else
            {
                return _database.InsertAsync(newCategory);
            }
        }

        public Task<int> DeleteCategoryAsync(Categories category)
        {
            return _database.DeleteAsync(category);
        }


        // RETETE
        public Task<Recipe> GetOneRecipe(string recipeName)
        {
            return _database.Table<Recipe>()
                    .Where(i => i.Name == recipeName)
                    .FirstAsync();
        }

        public Task<List<Recipe>> GetAllRecipes()
        {
            return _database.QueryAsync<Recipe>("SELECT * FROM Recipe");
        }

        public Task<List<Recipe>> GetAllRecipesbyId(int categoryID, int userID)
        {
            return _database.Table<Recipe>()
                        .Where(i => i.CategoryID == categoryID & i.UserID == userID)
                        .ToListAsync();
        }

        public Task<int> SaveRecipeAsync(Recipe newRecipe)
        {
            if (newRecipe.ID != 0)
            {
                return _database.UpdateAsync(newRecipe);
            }
            else
            {
                return _database.InsertAsync(newRecipe);
            }
        }

        public Task<int> DeleteRecipeAsync(Recipe recipe)
        {
            return _database.DeleteAsync(recipe);
        }

        public async void DeleteRecipesAsync(int categoryID)
        {
            List<Recipe> _myrecipe = await _database.Table<Recipe>().Where(i => i.CategoryID == categoryID).ToListAsync();
            if (_myrecipe != null)
            {
                foreach (Recipe recipe in _myrecipe)
                {
                    await _database.DeleteAsync(recipe);
                }
            }
        }

    }
}

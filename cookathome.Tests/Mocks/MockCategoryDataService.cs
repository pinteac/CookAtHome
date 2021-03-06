﻿using cootathome.Models;
using cootathome.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace cookathome.Tests.Mocks
{
    public class MockCategoryDataService : ICategoriesDataService
    {
        private List<Categories> mockCategories = new List<Categories>()
        {
            new Categories {  ID = 1, CategoryName = "Supe", ImageURL = "" },
            new Categories {  ID = 2, CategoryName = "Breakfast", ImageURL = "" },
            new Categories {  ID = 3, CategoryName = "Main Dish", ImageURL = ""},
            new Categories {  ID = 4, CategoryName = "Salate", ImageURL = ""},
            new Categories {  ID = 5, CategoryName = "Paste", ImageURL = ""},
         };

        public async Task<int> AddNewCategory(Categories newCategory)
        {

            if (newCategory.CategoryName == "ExceptionThrow")
                throw new Exception();
            else
            {
                var count = mockCategories.Count;
                mockCategories.Add(newCategory);

                if (mockCategories.Count == count + 1)
                    return 1;
                else
                    return 0;
            }
        }

        public Task<int> DeleteAsyncCategory(Categories categories)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCategoriesCount()
        {
            return mockCategories.Count;
        }

        public Task<Categories> GetAsyncCategory(string category)
        {
            throw new NotImplementedException();
        }

        Task<List<Categories>> ICategoriesDataService.GetAllCategories()
        {
            throw new NotImplementedException();
        }
    }
}

using cootathome.Models;
using System;
using System.Threading.Tasks;

namespace cootathome.Services
{
    public class UserDataService : IUserDataService
    {

        public async Task<User> GetAsyncTheUser(String username)
        {
                return await App.Database.GetUserByUserNameAsync(username);
        }

        public async Task<int> RegisterUser(User user)
        {
             return await App.Database.SaveUserAsync(user);
        }

    }
}

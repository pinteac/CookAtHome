using cootathome.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cootathome.Services
{
    public interface IUserDataService
    {
        Task<User> GetAsyncTheUser(string username);

        Task<int> RegisterUser(User user);
    }
}

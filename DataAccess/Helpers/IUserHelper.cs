using DataAccess.Entities;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers
{
     public interface IUserHelper
    {
        Task<UserInfo> GetUserByEmailAsync(string email);
        void AddUserAsync(UserInfo user);
        Task<bool> SaveAllAsync();
        IEnumerable<UserInfo> GetAll();
        Task<UserInfo> GetUser(string username, string pass);
        //Task CheckRoleAsync(string roleName);
        //Task<string> GetRoleAsync(User user);
        //Task AddUserToRoleAsync(User user, string roleName);
        //Task<bool> IsUserInRoleAsync(User user, string roleName);
        //Task<SignInResult> LoginAsync(LoginViewModel model);
        //Task LogoutAsync();
        //Task<SignInResult> ValidatePasswordAsync(User user, string password);

    }
}

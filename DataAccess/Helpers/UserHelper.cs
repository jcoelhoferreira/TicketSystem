using DataAccess.Entities;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _dataContext;

        public UserHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<UserInfo> GetUserByEmailAsync(string username)
        {
            return await _dataContext.UsersInfo.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<UserInfo> GetUser(string username, string pass)
        {
            return await _dataContext.UsersInfo
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == pass);
        }

        public void AddUserAsync(UserInfo user)
        {
            _dataContext.UsersInfo.Add(user);
        }

        public IEnumerable<UserInfo> GetAll()
        {
            return _dataContext.UsersInfo.ToList();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        //public async Task<string> GetRoleAsync(User user)
        //{
        //    var roles = await _userManager.GetRolesAsync(user);
        //    return roles.FirstOrDefault();
        //}

        //public async Task CheckRoleAsync(string roleName)
        //{
        //    var roleExists = await _roleManager.RoleExistsAsync(roleName);
        //    if (!roleExists)
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole
        //        {
        //            Name = roleName
        //        });
        //    }
        //}

        //public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        //{
        //    return await _userManager.IsInRoleAsync(user, roleName);
        //}

        //public async Task<SignInResult> LoginAsync(LoginViewModel model)
        //{
        //    return await _signInManager.PasswordSignInAsync(
        //        model.Email,
        //        model.Password,
        //        model.RememberMe,
        //        false);
        //}

        //public async Task LogoutAsync()
        //{
        //    await _signInManager.SignOutAsync();
        //}

        //public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        //{
        //    return await _signInManager.CheckPasswordSignInAsync(
        //        user,
        //        password,
        //        false);
        //}
    }
}

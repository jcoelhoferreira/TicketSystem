using AccountModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountModule.Services
{
    public class ApiUserService : IApiUserService
    {
        public Task<UserResponse> RegisterAsync(RegisterViewModel user)
        {
            return null;
        }
    }
}

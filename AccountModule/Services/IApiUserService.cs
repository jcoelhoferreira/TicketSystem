using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountModule.Services;
using AccountModule.ViewModels;

namespace AccountModule.Services
{
    public interface IApiUserService
    {
        Task<UserResponse> RegisterAsync(RegisterViewModel user);
    }
}

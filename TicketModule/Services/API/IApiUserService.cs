using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketModule.Models;
using TicketModule.Services;
using TicketModule.ViewModels;

namespace TicketModule.Services.API
{
    public interface IApiUserService
    {
        Task<ApiResponse> RegisterAsync(RegisterViewModel model);

        Task<ApiResponse> LoginUserAsync(LoginViewModel userInfo);
    }
}

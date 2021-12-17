using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketModule.Services;
using TicketModule.ViewModels;

namespace TicketModule.Services
{
    public interface IApiUserService
    {
        Task<ApiResponse> RegisterAsync(RegisterViewModel model);

        Task<ApiResponse> LoginAsync(LoginViewModel model);
    }
}

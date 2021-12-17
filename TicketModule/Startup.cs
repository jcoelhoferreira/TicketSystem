using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketModule.Services;

namespace TicketModule
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IApiTicketService, ApiTicketService>();
            services.AddScoped<IApiUserService, ApiUserService>();
            services.AddScoped<IEncryption, Encryption>();
        }
        
    }
}

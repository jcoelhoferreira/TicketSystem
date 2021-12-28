using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketModule.Services.API;
using TicketModule.Services;
using Microsoft.AspNetCore.Builder;

namespace TicketModule
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IApiTicketService, ApiTicketService>();
            services.AddScoped<IApiUserService, ApiUserService>();
            services.AddScoped<IEncryption, Encryption>();
            services.AddSession(cfg =>
            {
                cfg.IdleTimeout = TimeSpan.FromMinutes(20);
            });
        }

        public void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes)
        {
            app.UseSession();

            routes.MapAreaControllerRoute
            (
                name: "Home",
                areaName: "TicketModule",
                pattern: "",
                defaults: new { controller = "Account", action = "Login" }
            );
        }
    }
}

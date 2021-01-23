using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryUI.Common.Model;
using InventoryUI.DataAccess.Repository;
using InventoryUI.DataAccess.Repository.Interfaces;
using InventoryUI.Infrastructure.Service;
using InventoryUI.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InventoryUI.Presentation
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services
                .AddOptions()
                .Configure<Config>(Configuration.GetSection("Config"))
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"))
                        .AddConsole()
                        .AddDebug();
                })
                .AddHttpClient()
                .AddSingleton<IConfigurationRoot>(Configuration)
                .AddTransient<IRoleRepository, RoleRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IUserService, UserService>();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}

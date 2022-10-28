using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DataLayer.DbContext;
using Dependency = IoC.Ioc.Dependency;
namespace Eshop
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthorization();
            #region Authentication

            services.AddAuthentication(optoin =>
            {
                optoin.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                optoin.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                optoin.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option =>
            {
                option.LoginPath = "/Login";
                option.LogoutPath = "/Logout";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
            });

            #endregion
            services.AddDbContext<ApplicationDbContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

                }
            );

            Dependency.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/ErrorHandler/500");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

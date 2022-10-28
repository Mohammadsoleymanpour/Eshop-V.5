

//using DataLayer.DbContext;
//using IoC.Ioc;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
//using Microsoft.EntityFrameworkCore;
//using Dependency = IoC.Ioc.Dependency;


//var builder = WebApplication.CreateBuilder(args);
////StatUp
//// Add services to the container.


//builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});
//builder.Services.AddAuthentication(option =>
//{
//    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddCookie(option =>
//{
//    option.LoginPath = "/Login";
//    option.LogoutPath = "/Logout";
//    option.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
//});

//Dependency.RegisterServices(builder.Services);

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/ErrorHandler/500");
//    app.UseHsts();
//}

//app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();


//app.MapControllerRoute(
//            name: "area",
//            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//          );


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}"

//    );


//app.Run();

using Eshop;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
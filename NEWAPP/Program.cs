//using Data.Repositories.Implemention;
//using Data.Repositories.Interfaces;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers().AddNewtonsoftJson();

//builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<IClient, ClientRepository>();
//builder.Services.AddScoped<ICompanyUsers, CompanyUsersRepository>();
//builder.Services.AddScoped<IEmployees, EmployeesRepository>();
//builder.Services.AddScoped<ITab, TabRepository>();
//builder.Services.AddScoped<IManager, ManagerRepository>();
//builder.Services.AddScoped<IDefectiveProducts, DefectiveProductsRepository>();
//builder.Services.AddScoped<IAdminUsers, AdminUsersRepository>();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)


//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = "your-issuer",
//            ValidAudience = "your-audience",
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
//        };
//    });

//builder.Services.AddDistributedMemoryCache(); // This stores session in memory
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(10); // Set the session timeout
//    options.Cookie.HttpOnly = true; // Make the session cookie HTTP-only
//    options.Cookie.IsEssential = true; // Make the session cookie essential
//});
//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();
//app.UseSession();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using startupfile;

namespace YourNamespace
{
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
}

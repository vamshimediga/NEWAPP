using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
using NEWAPP.Filters.ActionFilters;
using OfficeOpenXml;
using NEWAPP.Filters.AuthorizationFilter;
using NEWAPP.Filters.ExceptionFilter;


namespace startupfile
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
          services.AddControllersWithViews().AddNewtonsoftJson();
          services.AddHttpClient();
         
          services.AddScoped<CustomExceptionFilter>();
          services.AddScoped<CustomAuthorizationFilte>();
          services.AddScoped<IClient, ClientRepository>();
          services.AddScoped<ICompanyUsers, CompanyUsersRepository>();
          services.AddScoped<IEmployees, EmployeesRepository>();
          services.AddScoped<ITab, TabRepository>();
          services.AddScoped<IManager, ManagerRepository>();
          services.AddScoped<IDefectiveProducts, DefectiveProductsRepository>();
          services.AddScoped<IAdminUsers, AdminUsersRepository>();
          services.AddScoped<ICommonService, CommonServiceRepository>(); 
          services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
          services.AddScoped<CustomActionFilter>();
          services.AddScoped<IFile, FileRepository>();
          services.AddScoped<IProductChart, ProductChartRepository>();
          services.AddScoped<IHRMGER, HRMGERRepository>();
          services.AddScoped<IUserradiochkdrop,UserradiochkdropRepository>();
          services.AddScoped<IProjectInfo, ProjectInfoRepository>();
          services.AddScoped<IOrdersWithCustomer, OrdersWithCustomerRepository>();
          services.AddScoped<IStockoverflow, StockoverflowRepository>();
          services.AddScoped<IUserAdmin, UserAdminRepository>();
          services.AddScoped<IMAdmin,MAdminRepository>();
          services.AddScoped<IPsplCompany, PsplCompanyReposisitory>();
          services.AddScoped<IMediaWithCategory, MediaWithCategoryRepository>();
          services.AddScoped<IRestaurant, RestaurantRepository>();
          services.AddScoped<ILeads, LeadsRepository>();
          services.AddScoped<ICourse, CourseRepository>();
            services.AddScoped<ISampleStudent, SampleStudentRepository>();
          services.AddControllersWithViews(options =>
            {
                options.Filters.Add<CustomActionFilter>(); // Apply the filter globally
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "your-issuer",
                        ValidAudience = "your-audience",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
                    };
                });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

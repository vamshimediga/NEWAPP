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
using Microsoft.AspNetCore.Http.Features;
using NEWAPP;
using DomainModels;


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
            services.AddAutoMapper(typeof(MappingProfile));
            // Add TokenService to the DI container
            services.AddSingleton<TokenService>(new TokenService("A4e1eYB4WmI1D9Cj/NBzNp1HzNs30WqO2yZVoUmD8GQ="));
            services.AddControllersWithViews().AddNewtonsoftJson();
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            services.AddLogging();
            services.AddSingleton<ExceptionLogger>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddHttpClient();
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600; // 100 MB
            });
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
          services.AddScoped<ICustomerDATARepository<CustomerData>, CustomerDATARepository>();
          services.AddScoped<IHumanResources, HumanResourcesRepository>();
          services.AddScoped<ILeadSource, LeadSourceRepository>();
          services.AddScoped<IUserData, UserDataRepository>();
          services.AddScoped<IPersonData, PersonDataRepository>();
          services.AddScoped<ICompany, CompanyRepository>();
          services.AddScoped<IContect, ContectRepository>();
          services.AddScoped<Ionline_retailUserLogin, online_retailUserLoginRepository>();
          services.AddScoped<IAccessTable, AccessTableRepository>();
          services.AddScoped<IUserLogin, UserLoginRepository>();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<CustomActionFilter>(); // Apply the filter globally
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
                        name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

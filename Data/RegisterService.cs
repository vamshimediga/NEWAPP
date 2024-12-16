using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class RegisterService
    {
        public static void BindServices(this IServiceCollection service)
        {

            service.AddScoped<IClient, ClientRepository>();
            service.AddScoped<ICompanyUsers, CompanyUsersRepository>();
            service.AddScoped<IEmployees, EmployeesRepository>();
            service.AddScoped<ITab, TabRepository>();
            service.AddScoped<IManager, ManagerRepository>();
            service.AddScoped<IDefectiveProducts, DefectiveProductsRepository>();
            service.AddScoped<IAdminUsers, AdminUsersRepository>();
            service.AddScoped<IFile, FileRepository>();
            service.AddScoped<IProductChart, ProductChartRepository>();
            service.AddScoped<IHRMGER, HRMGERRepository>();
            service.AddScoped<IUserradiochkdrop, UserradiochkdropRepository>();
            service.AddScoped<IProjectInfo, ProjectInfoRepository>();
            service.AddScoped<IOrdersWithCustomer, OrdersWithCustomerRepository>();
            service.AddScoped<IStockoverflow, StockoverflowRepository>();
            service.AddScoped<IUserAdmin, UserAdminRepository>();
            service.AddScoped<IMAdmin,MAdminRepository>();
            service.AddScoped<IPsplCompany,PsplCompanyReposisitory>();
            service.AddScoped<IMediaWithCategory, MediaWithCategoryRepository>();
            service.AddScoped<IRestaurant, RestaurantRepository>();
            service.AddScoped<ILeads, LeadsRepository>();   
            service.AddScoped<ICourse, CourseRepository>(); 
            service.AddScoped<ISampleStudent, SampleStudentRepository>();
            service.AddScoped<ICustomerDATARepository<CustomerData>, CustomerDATARepository>();
            service.AddScoped<IHumanResources, HumanResourcesRepository>();
            service.AddScoped<ILeadSource, LeadSourceRepository>();
            service.AddScoped<IUserData, UserDataRepository>();
            service.AddScoped<IPersonData, PersonDataRepository>();
            service.AddScoped<ICompany, CompanyRepository>();
            service.AddScoped<IContect, ContectRepository>();
            service.AddScoped<Ionline_retailUserLogin,online_retailUserLoginRepository>();
            service.AddScoped<IAccessTable, AccessTableRepository>();
            service.AddScoped<IUserLogin, UserLoginRepository>();
            service.AddScoped<ICookingRecipe, CookingRecipeRepository>();
            service.AddScoped<IITInstitute, IITInstituteRepository>();
            service.AddScoped<IAuthor, AuthorRepository>();
            service.AddScoped<IPsplCustomer, PsplCustomerRepository>();
            service.AddScoped<IPsplClient , PsplClientRepository>();
            service.AddScoped<IAuthor_US, Author_USRepository>();
        }
    }
}

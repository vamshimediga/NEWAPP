﻿using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
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
        }
    }
}
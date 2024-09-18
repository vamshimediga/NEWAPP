using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork: IUnitOfWork
    {
       

        public UnitOfWork(IClient _clientService)
        {
            clientRepository = _clientService;
        }

        public IClient clientRepository { get; }

        public ICompanyUsers companyUsers { get; }

        public IEmployees employees { get; }

        public ITab tab { get; }

        public IManager manager { get; }

        public IDefectiveProducts   defectiveProducts { get; }

        public IAdminUsers adminUsers { get; }

        public IFile file { get; }
        public IProductChart productChart { get; }

        public IHRMGER hRMGER { get; }

        public IUserradiochkdrop userradiochkdrop { get; }

        public IProjectInfo projectInfo { get; }

        public IOrdersWithCustomer orders{ get; }

        public IStockoverflow stockoverflow { get; }

        public IAdminUsers userAdmin { get; }

       public IUserAdmin uAdmin {  get; }

        public IMAdmin mAdmin { get; }

        public IPsplCompany psplCompany { get; }

        public IMediaWithCategory media { get; }

        public IRestaurant restaurant { get; }

        public ILeads leads { get; }

        public ICourse  course { get; }

       
        public ISampleStudent smpleStudent { get; }

        public ICustomerDATARepository<CustomerData> CustomerDATARepository { get; }

        public IHumanResources humanResources { get; }

        public ILeadSource leadSource { get; }
    }
}

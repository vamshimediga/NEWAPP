using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICompanyUsers
    {
        Task<List<CompanyUsers>> GetCompanyUsers();
        Task<CompanyUsers> GetCompanyUsersbyid(int id);

        Task<int> insertCompanyUsers(CompanyUsers companyUsers);  
        Task<int> updateCompanyUsers(CompanyUsers companyUsers);
        Task<bool> deleteCompanyUsers(int id);
    }
}

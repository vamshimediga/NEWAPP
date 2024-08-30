using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IAdminUsers
    {
        Task<List<AdminUser>> GetAdminUsersAsync();
        Task<AdminUser> GetAdminUserByIdAsync(int Id);
        Task<int> InsertAdminUser(AdminUser adminUser); 
        Task<int> UpdateAdminUserAsync(AdminUser adminUser);
        Task<int> DeleteAdminUserAsync(int Id);

    }
}

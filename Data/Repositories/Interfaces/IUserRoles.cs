using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserRoles
    {
        Task<List<UserRoles>> GetUserRoles();
        Task<UserRoles> GetUserRolesbyid(int id);

        Task<int> insert(UserRoles userRoles);  

        Task<int> update(UserRoles userRoles);

        Task<int> delete(int id);   
    }
}

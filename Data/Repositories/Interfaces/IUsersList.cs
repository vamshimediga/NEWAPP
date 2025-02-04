using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUsersList
    {
        Task<List<UsersList>> GetUsers();
        Task<UsersList> GetUsersById(int id);

        Task<int> insert(UsersList usersList);
        
        Task<int> update(UsersList usersList);

        Task<int> delete(int id);
    }
}

using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserAdmin
    {
        List<UserAdmin> GetUserAdmins();
        UserAdmin GetUserAdminById(int id);

        int InsertUserAdmin(UserAdmin userAdmin);
        int UpdateUserAdmin(UserAdmin userAdmin);
        int DeleteUserAdmin(int id);
       
    }
}

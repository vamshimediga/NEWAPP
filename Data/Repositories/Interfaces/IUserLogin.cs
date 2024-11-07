using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserLogin
    {
        Task<List<UserLogin>> UserLogins();

        Task<UserLogin> UserLoginAsync(string id);

        Task<string> insert(UserLogin userLogin);
        Task<string> update(UserLogin userLogin);
        Task<string> delete(string id);

    }
}

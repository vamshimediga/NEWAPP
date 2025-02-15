using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface Ionline_retailUserLogin
    {
        Task<List<online_retailUserLogin>> GetOnline_RetailUserLogins();
        Task<online_retailUserLogin> GetOnline_RetailUserLogin(int id);

        Task<int> insert(online_retailUserLogin online_retailUserLogin);
        Task<int> update(online_retailUserLogin online_retailUserLogin);

        Task<bool> Delet(int id);
    }
}

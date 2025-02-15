using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IAccessTable
    {

        Task<List<AccessTable>> GetAllAccessTablesAsync();

        Task<AccessTable> GetAccessTableByIdAsync(int id);

        Task<int> insert(AccessTable accessTable);
        Task<int> update(AccessTable accessTable);
        Task<int> delete(int id);   


    }
}

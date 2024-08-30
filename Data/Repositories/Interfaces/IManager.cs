using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IManager
    {
        List<Manager> GetManagers();

        Manager GetManager(int id);
        int Insert(Manager manager);

        int Update(Manager manager);

        bool Delete(List<int> ids);
    }
}

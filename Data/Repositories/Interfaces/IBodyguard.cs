using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IBodyguard
    {
        Task<List<Bodyguard>> Get();
        Task<Bodyguard> GetByid(int id);

        Task<int> insert(Bodyguard bodyguard);

        Task<int> update(Bodyguard bodyguard);

        Task<int> delete(int id);
    }
}

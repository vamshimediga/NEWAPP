using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IFlat
    {
        Task<List<Flat>> GetFlatsAsync();   

        Task<Flat> GetFlatByIdAsync(int id);

        Task Insert(Flat item);

        Task Update(Flat item);

        Task Delete(int id);
    }
}

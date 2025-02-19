using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IConstructionBuilder
    {
        Task<List<ConstructionBuilder>> Get();

        Task<ConstructionBuilder> GetConstructorById(int id);

        Task Insert(ConstructionBuilder builder);

        Task Update(ConstructionBuilder builder);

        Task Delete(int cid);


    }
}

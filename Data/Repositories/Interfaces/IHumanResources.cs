using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IHumanResources
    {
        Task<List<HumanResources>> GetHumanResources();
        Task<HumanResources>GetHumanResourcesById(int Id);
        Task<int> Insert(HumanResources humanResources);
        Task<int> Update(HumanResources humanResources);

        Task<bool> Delete(string ids);
    }
}

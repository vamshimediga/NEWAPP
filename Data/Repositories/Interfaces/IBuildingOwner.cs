using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IBuildingOwner
    {
        Task<List<BuildingOwner>> GetAll();

        Task<BuildingOwner> GetById(int id);

        Task Insert(BuildingOwner entity);

        Task Update(BuildingOwner entity);

        Task Delete(int id);
    }
}

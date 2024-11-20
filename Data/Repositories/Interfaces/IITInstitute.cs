using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IITInstitute
    {

        Task<List<ITInstitute>> GetITInstitutes();
        Task<ITInstitute> GetById(int id);

        Task<int> insert(ITInstitute institute);

        Task<int> update(ITInstitute institute);
        Task<int> delete(int id);   


    }
}

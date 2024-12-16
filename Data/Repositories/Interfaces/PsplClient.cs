using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IPsplClient
    {
        Task<List<PsplClient>> GetAll();
        Task<PsplClient> GetById(int id);   

        Task<int> Insert(PsplClient psplClient);    

        Task<int> Update(PsplClient psplClient);

        Task DeleteById(int id);

       


    }
}

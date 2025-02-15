using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IAuthor_US
    {
        Task<List<Author_US>> GetAll();

        Task<Author_US> GetById(int id);

        Task<int> Insert(Author_US entity);

        Task<int> Update(Author_US entity);

        Task<int> DeleteById(int id);



    }
}

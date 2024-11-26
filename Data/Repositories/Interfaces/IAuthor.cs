using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IAuthor
    {
        Task<List<Author>> GetAuthors();
        Task<Author> GetByid(int id);
        Task insert(Author author);
        Task<int> update(Author author);    
        Task<int> delete(int id);   

    }
}

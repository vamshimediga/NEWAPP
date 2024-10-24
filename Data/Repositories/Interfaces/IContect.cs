using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IContect
    {
        Task<List<Contect>> GetContects();
        Task<Contect> GetContectById(int id);   
        Task<int> insert(Contect contect);  
        Task<int> update(Contect contect);
        Task<int> delete(int id);

        Task<List<Contect>> SearchContectByFirstNameAsync(string firstName);
    }
}

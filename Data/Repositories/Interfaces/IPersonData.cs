using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IPersonData
    {
        Task<List<PersonData>> GetPersonList();
        Task<PersonData> GetPersonById(int id); 
        Task<int> insertPerson(PersonData person);
        Task<int> updatePerson(PersonData person);
        Task<int> deletePerson(int id);

    }
}

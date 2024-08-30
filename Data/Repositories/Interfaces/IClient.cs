using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IClient
    {
        Task<List<Client>> GetClients();
        Task<Client> GetClientById(int id);
        Task<int> InsertClient(Client client);

        Task<bool> DeleteClients(int[] ids);


        Task<int> UpdateClientDetails(Client client);
    }
}

using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IPsplCustomer
    {
        Task<List<PsplCustomer>> GetPsplCustomers();
        
        Task<PsplCustomer> GetPsplCustomersByidAsync(int id);
    }
}

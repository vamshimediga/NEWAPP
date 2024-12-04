using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class PsplCustomerRepository :BaseRepository, IPsplCustomer
    {
        public PsplCustomerRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<List<PsplCustomer>> GetPsplCustomers()
        {
          return  await QueryAsync<PsplCustomer>("[dbo].[GetPsplCustomers]");
        }

        public async Task<PsplCustomer> GetPsplCustomersByidAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerID", id);
            PsplCustomer psplCustomer = await QueryFirstOrDefaultAsync<PsplCustomer>("GetPsplCustomerById", parameters);
            return psplCustomer; // 
        }
    }
}

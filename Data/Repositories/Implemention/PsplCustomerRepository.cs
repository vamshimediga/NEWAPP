using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
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

        public async Task update(PsplCustomer customer)
        {
            var parameters = new DynamicParameters();
            // Add parameters for the stored procedure
            parameters.Add("CustomerID", customer.CustomerID, DbType.Int32);
            parameters.Add("FirstName", customer.FirstName, DbType.String);
            parameters.Add("LastName", customer.LastName, DbType.String);
            parameters.Add("Email", customer.Email, DbType.String);
            parameters.Add("PhoneNumber", customer.PhoneNumber, DbType.String);
            parameters.Add("Address", customer.Address, DbType.String);
            parameters.Add("City", customer.City, DbType.String);
            parameters.Add("State", customer.State, DbType.String);
            parameters.Add("ZipCode", customer.ZipCode, DbType.String);
            parameters.Add("ModifiedDate", customer.ModifiedDate, DbType.DateTime);
            // Execute the stored procedure using DynamicParameters
            await ExecuteAsync("dbo.UpdateCustomer", parameters);
        }
    }
}

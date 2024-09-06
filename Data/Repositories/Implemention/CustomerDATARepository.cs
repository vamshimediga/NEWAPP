using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class CustomerDATARepository : ICustomerDATARepository<CustomerData>
    {
        public readonly IDbConnection connection;
        
        public CustomerDATARepository(IConfiguration configuration) { 
        connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        } 

        public  Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerData>> GetAllAsync()
        {
            List<CustomerData> customerDatas = (List<CustomerData>)await connection.QueryAsync<CustomerData>("GetAllCustomers");
            return customerDatas;
        }

        public Task<CustomerData> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async  Task<int> InsertAsync(CustomerData entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", entity.FirstName);
            parameters.Add("@LastName", entity.LastName);
            parameters.Add("@Email", entity.Email);
            parameters.Add("@PhoneNumber", entity.PhoneNumber);
            parameters.Add("@CustomerID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            // Execute the stored procedure
            await  connection.ExecuteAsync("InsertCustomer", parameters);
            int id = parameters.Get<int>("@CustomerID");
            return id;

        }

        public  Task<int> UpdateAsync(CustomerData entity)
        {
            throw new NotImplementedException();
        }
    }
}

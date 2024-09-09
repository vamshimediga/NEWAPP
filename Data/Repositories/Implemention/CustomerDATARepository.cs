using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Data.Repositories.Implemention
{
    public class CustomerDATARepository : ICustomerDATARepository<CustomerData>
    {
        public readonly IDbConnection connection;
        
        public CustomerDATARepository(IConfiguration configuration) { 
        connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        } 

        public async  Task<bool> DeleteAsync(int id)
        {
            string ids=  string.Join(",", id);
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@CustomerIDsToDelete", ids);
            int affectedRows = await connection.ExecuteAsync("DeleteMultipleCustomers", dynamicParameters);
            return true;  
        }

        public async Task<List<CustomerData>> GetAllAsync()
        {
            List<CustomerData> customerDatas = (List<CustomerData>)await connection.QueryAsync<CustomerData>("GetAllCustomers");
            return customerDatas;
        }

        public async  Task<CustomerData> GetByIdAsync(int id)
        {
            CustomerData CustomerData = await connection.QueryFirstOrDefaultAsync<CustomerData>("GetCustomerByID", new { @CustomerID = id });
            return CustomerData;
          
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


        public async Task<int> UpdateAsync(CustomerData entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerID", entity.CustomerID, DbType.Int32, ParameterDirection.InputOutput);
            parameters.Add("@FirstName", entity.FirstName);
            parameters.Add("@LastName", entity.LastName);
            parameters.Add("@Email", entity.Email);
            parameters.Add("@PhoneNumber", entity.PhoneNumber);

            // Execute the stored procedure
            await connection.ExecuteAsync("UpdateCustomerData", parameters);

            // Retrieve the updated CustomerID (Output parameter)
            int id = parameters.Get<int>("@CustomerID");
            return id;
        }

    }
}

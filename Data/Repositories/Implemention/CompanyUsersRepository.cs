using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class CompanyUsersRepository : ICompanyUsers
    {
        private readonly DbConnection _dbContext;
        private readonly IConfiguration _config;
        private readonly string ConnectionStrings = "DefaultConnection";
        public CompanyUsersRepository(IConfiguration config)
        {
            _config = config;
        }
        private DbConnection GetDbConnection()
        {
            return new SqlConnection(_config.GetConnectionString(ConnectionStrings));
        }
        public async Task<List<CompanyUsers>> GetCompanyUsers()
        {
            try
            {
                DbConnection dbConnection = GetDbConnection();
                await dbConnection.OpenAsync();
                List<CompanyUsers> CompanyUsers = (List<CompanyUsers>)await dbConnection.QueryAsync<CompanyUsers>("GETCompanyUsers", commandType: CommandType.StoredProcedure);
                dbConnection.Close();
                return CompanyUsers;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<CompanyUsers> GetCompanyUsersbyid(int id)
        {
            DbConnection dbConnection = GetDbConnection();
            await dbConnection.OpenAsync();
            CompanyUsers companyUsers = await dbConnection.QueryFirstOrDefaultAsync<CompanyUsers>("GetCompanyUsersById", new { @UserId = id }, commandType: CommandType.StoredProcedure);
            return companyUsers;
        }

        public async Task<int> insertCompanyUsers(CompanyUsers companyUsers)
        {
            try
            {
                DbConnection dbConnection = GetDbConnection();
                await dbConnection.OpenAsync();

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Username", companyUsers.username);
                dynamicParameters.Add("@UserId", DbType.Int32, direction: ParameterDirection.Output); // Output parameter

                // Execute the stored procedure to insert company user and retrieve the output parameter
                await dbConnection.ExecuteAsync("InsertCompanyUser", dynamicParameters, commandType: CommandType.StoredProcedure);

                int userId = dynamicParameters.Get<int>("@UserId"); // Retrieve the value of output parameter

                return userId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> updateCompanyUsers(CompanyUsers companyUsers)
        {
            try
            {
                DbConnection dbConnection = GetDbConnection();
                await dbConnection.OpenAsync();

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@userid", companyUsers.userid); // Output parameter
                dynamicParameters.Add("@username", companyUsers.username);
                dynamicParameters.Add("@Id", DbType.Int32, direction: ParameterDirection.Output);
                // Execute the stored procedure to insert company user and retrieve the output parameter
                await dbConnection.ExecuteAsync("UpdateCompanyUsers", dynamicParameters, commandType: CommandType.StoredProcedure);
                int userId = dynamicParameters.Get<int>("@Id"); // Retrieve the value of output parameter

                return userId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> deleteCompanyUsers(int id)
        {
            DbConnection dbConnection = GetDbConnection();
            await dbConnection.OpenAsync();
            int Id = await dbConnection.ExecuteAsync("DeletetCompanyUser", new { @UserId = id }, commandType: CommandType.StoredProcedure);
            bool success = Id > 0;
            return success;
        }
    }
}

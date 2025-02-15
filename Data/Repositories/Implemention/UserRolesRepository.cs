using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Data.Repositories.Implemention
{
    public class UserRolesRepository : IUserRoles
    {
        private readonly IDbConnection _dbConnection;

        public UserRolesRepository(IConfiguration configuration)
        {
            // Initialize DB connection with connection string from IConfiguration
            _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        // Delete UserRole by ID
        public async Task<int> delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleID", id, DbType.Int32);

            var result = await _dbConnection.ExecuteAsync("dbo.DeleteUserRole", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        // Get all UserRoles
        public async Task<List<UserRoles>> GetUserRoles()
        {
            List<UserRoles> result = (List<UserRoles>)await _dbConnection.QueryAsync<UserRoles>("[dbo].[GetUserRole]");
            return result.ToList();
        }

        // Get UserRole by ID
        public async Task<UserRoles> GetUserRolesbyid(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleID", id, DbType.Int32);

            var result = await _dbConnection.QueryFirstOrDefaultAsync<UserRoles>("dbo.GetUserRoleByID", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        // Insert a new UserRole
        public async Task<int> insert(UserRoles userRoles)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleName", userRoles.RoleName, DbType.String);

            var result = await _dbConnection.ExecuteScalarAsync<int>("dbo.InsertUserRole", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        // Update an existing UserRole
        public async Task<int> update(UserRoles userRoles)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleID", userRoles.RoleID, DbType.Int32);
            parameters.Add("@RoleName", userRoles.RoleName, DbType.String);

            var result = await _dbConnection.ExecuteAsync("dbo.UpdateUserRole", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}

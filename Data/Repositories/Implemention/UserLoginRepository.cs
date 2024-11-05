using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class UserLoginRepository : IUserLogin
    {
        public readonly DbConnection _connection;
        public UserLoginRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Task<int> delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> insert(UserLogin userLogin)
        {
            throw new NotImplementedException();
        }

        public async Task<string> update(UserLogin userLogin)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userLogin.user_id);
            parameters.Add("@UserPassword", userLogin.user_password, DbType.String,  ParameterDirection.Input);
            parameters.Add("@FirstName", userLogin.first_name, DbType.String, ParameterDirection.Input);
            parameters.Add("@LastName", userLogin.last_name, DbType.String, ParameterDirection.Input);
            parameters.Add("@EmailID", userLogin.email_id, DbType.String, ParameterDirection.Input);
            parameters.Add("@UpdatedUserID", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);
            // Execute the stored procedure
            _connection.Execute("railway_system.UpdateUser", parameters);
            // Retrieve the output parameter value
            string  updatedUserId = parameters.Get<string>("@UpdatedUserID");
            return updatedUserId;
        }

        public async Task<UserLogin> UserLoginAsync(string id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", id, DbType.String);

            // Execute the stored procedure and map the result to the UserLogin model
            var user = await _connection.QueryFirstOrDefaultAsync<UserLogin>("[railway_system].[GetUserById]",parameters);

            return user;
        }

        public async Task<List<UserLogin>> UserLogins()
        {
            return (List<UserLogin>)await _connection.QueryAsync<UserLogin>("[railway_system].[GetTopUsers]");
        }
    }
}

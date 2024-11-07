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

       
        public async Task<string> delete(string id)
        {
            // Execute the stored procedure
            var parameters = new { UserID = id };

            // Dapper executes the procedure and returns the number of affected rows
            var rowsAffected = await _connection.ExecuteAsync("railway_system.DeleteUser", parameters);

            // Return true if the deletion was successful (i.e., at least one row was affected)
            return  rowsAffected > 0 ? Convert.ToString(rowsAffected) : "1";
        }

        public async Task<string> insert(UserLogin userLogin)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userLogin.user_id, DbType.String);
            parameters.Add("@UserPassword", userLogin.user_password, DbType.String);
            parameters.Add("@FirstName", userLogin.first_name, DbType.String);
            parameters.Add("@LastName", userLogin.last_name, DbType.String);
            parameters.Add("@EmailID", userLogin.email_id, DbType.String);

            // Output parameter to get the created UserID
            parameters.Add("@CreatedUserID", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

            // Call the stored procedure
            await _connection.ExecuteAsync("[railway_system].[InsertUser]", parameters);

            // Retrieve the value of the output parameter
            string createdUserId = parameters.Get<string>("@CreatedUserID");
            return createdUserId;
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

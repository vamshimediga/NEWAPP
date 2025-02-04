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
    public class UsersListRepository : IUsersList
    {
        private readonly IDbConnection _dbConnection;
        private readonly IUserRoles _userRole;
        public UsersListRepository(IConfiguration configuration, IUserRoles userRole)
        {
            // Initialize DB connection with connection string from IConfiguration
            _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _userRole = userRole;
        }

        // Get All Users
        public async Task<List<UsersList>> GetUsers()
        {
            try
            {


                // Query all users via stored procedure
                List<UsersList> result = (List<UsersList>)await _dbConnection.QueryAsync<UsersList>(
                    "GetAllUsers", commandType: CommandType.StoredProcedure);
            
                return result.ToList();
            }
            catch (Exception ex)
            {
                // Handle exception if any
                throw new Exception("An error occurred while retrieving users", ex);
            }
            finally
            {
                // Ensure the connection is closed after use
                _dbConnection.Close();
            }
        }

        // Get User by ID
        public async Task<UsersList> GetUsersById(int id)
        {
            try
            {
                // Open connection
                _dbConnection.Open();

                // Create DynamicParameters and add ID
                var parameters = new DynamicParameters();
                parameters.Add("UserID", id, DbType.Int32);

                // Query user by ID via stored procedure
                var result = await _dbConnection.QuerySingleOrDefaultAsync<UsersList>(
                    "GetUserByID", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                // Handle exception if any
                throw new Exception("An error occurred while retrieving the user by ID", ex);
            }
            finally
            {
                // Ensure the connection is closed after use
                _dbConnection.Close();
            }
        }

        // Insert a New User
        public async Task<int> insert(UsersList usersList)
        {
            try
            {
                // Open connection
                _dbConnection.Open();

                // Create DynamicParameters and add values
                var parameters = new DynamicParameters();
                parameters.Add("UserName", usersList.UserName, DbType.String);
                parameters.Add("Email", usersList.Email, DbType.String);
                parameters.Add("RoleID", usersList.RoleID, DbType.Int32);

                // Insert a new user via stored procedure
                var result = await _dbConnection.ExecuteAsync("InsertUser", parameters, commandType: CommandType.StoredProcedure);
                return result; // Returns the number of affected rows
            }
            catch (Exception ex)
            {
                // Handle exception if any
                throw new Exception("An error occurred while inserting the user", ex);
            }
            finally
            {
                // Ensure the connection is closed after use
                _dbConnection.Close();
            }
        }

        // Update an Existing User
        public async Task<int> update(UsersList usersList)
        {
            try
            {
                // Open connection
                _dbConnection.Open();

                // Create DynamicParameters and add values
                var parameters = new DynamicParameters();
                parameters.Add("UserID", usersList.UserID, DbType.Int32);
                parameters.Add("UserName", usersList.UserName, DbType.String);
                parameters.Add("Email", usersList.Email, DbType.String);
                parameters.Add("RoleID", usersList.RoleID, DbType.Int32);

                // Update user via stored procedure
                var result = await _dbConnection.ExecuteAsync("UpdateUser", parameters, commandType: CommandType.StoredProcedure);
                return result; // Returns the number of affected rows
            }
            catch (Exception ex)
            {
                // Handle exception if any
                throw new Exception("An error occurred while updating the user", ex);
            }
            finally
            {
                // Ensure the connection is closed after use
                _dbConnection.Close();
            }
        }

        // Delete User
        public async Task<int> delete(int id)
        {
            try
            {
                // Open connection
                _dbConnection.Open();

                // Create DynamicParameters and add values
                var parameters = new DynamicParameters();
                parameters.Add("UserID", id, DbType.Int32);

                // Delete user via stored procedure
                var result = await _dbConnection.ExecuteAsync("DeleteUser", parameters, commandType: CommandType.StoredProcedure);
                return result; // Returns the number of affected rows
            }
            catch (Exception ex)
            {
                // Handle exception if any
                throw new Exception("An error occurred while deleting the user", ex);
            }
            finally
            {
                // Ensure the connection is closed after use
                _dbConnection.Close();
            }
        }
    }
}

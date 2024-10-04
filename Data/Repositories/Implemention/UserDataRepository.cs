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
    public class UserDataRepository : IUserData
    {
        public readonly DbConnection _connection;
        public UserDataRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task InsertUserData(UserData userData)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", userData.UserName);
            parameters.Add("@Email", userData.Email);
            parameters.Add("@Address", userData.Profile.Address);
            parameters.Add("@PhoneNumber", userData.Profile.PhoneNumber);
            await _connection.ExecuteAsync("InsertUserAndProfile", parameters);
        }

        public async Task UpdateUserData(UserData userData)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userData.UserID);
            parameters.Add("@UserName", userData.UserName);
            parameters.Add("@Email", userData.Email);
            parameters.Add("@Address", userData.Profile.Address);
            parameters.Add("@PhoneNumber", userData.Profile.PhoneNumber);
            await _connection.ExecuteAsync("UpdateUserAndProfile", parameters);
        }

        public async Task<List<UserData>> UserDataAsync()
        {
            var sql = @"
            SELECT 
                u.UserID, 
                u.UserName, 
                u.Email, 
                up.ProfileID, 
                up.Address, 
                up.PhoneNumber,
                up.UserID
            FROM UsersData u
            INNER JOIN UserProfiles up
                ON u.UserID = up.UserID";


            // Query the database and map the user and profile
            List<UserData> userWithProfile = (List<UserData>)await _connection.QueryAsync<UserData, UserProfile, UserData>(
                    sql,
                    (user, profile) =>
                    {
                        user.Profile = profile;  // Assign the profile to the user object
                        return user;             // Return the user with the profile
                    },
                    // Pass the UserID parameter
                    splitOn: "ProfileID"         // Specify where to split between user and profile
                );

                return userWithProfile; // Return the first result or null
            
        }

        public async Task<UserData> UserDataById(int id)
        {
            var sql = @"
         SELECT 
        u.UserID, 
        u.UserName, 
        u.Email, 
        up.ProfileID, 
        up.Address, 
        up.PhoneNumber,
        up.UserID
    FROM UsersData u
    INNER JOIN UserProfiles up
        ON u.UserID = up.UserID
    WHERE u.UserID = @UserID"; // Parameterized query to prevent SQL injection

            // Query the database and map the user and profile
            var userWithProfile = (await _connection.QueryAsync<UserData, UserProfile, UserData>(
                sql,
                (user, profile) =>
                {
                    user.Profile = profile;  // Assign the profile to the user object
                    return user;             // Return the user with the profile
                },
                param: new { UserID = id },  // Pass the UserID parameter
                splitOn: "ProfileID"         // Specify where to split between user and profile
            )).FirstOrDefault();            // Get the first or default result

            return userWithProfile;
        }

    }
}

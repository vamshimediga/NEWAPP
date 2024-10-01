using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
    }
}

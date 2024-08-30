using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class UserAdminRepository : IUserAdmin
    {
        public readonly IDbConnection _connection;  
        public UserAdminRepository(IConfiguration configuration ) {

            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }    
        public int DeleteUserAdmin(int id)
        {
            throw new NotImplementedException();
        }

        public UserAdmin GetUserAdminById(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserAdmin> GetUserAdmins()
        {
            List<UserAdmin> userAdmins = _connection.Query<UserAdmin>("GetUsersAdmin").ToList();
            return userAdmins;
        }

        public int InsertUserAdmin(UserAdmin userAdmin)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@username", userAdmin.Username);
            parameters.Add("@password", userAdmin.Password);
            parameters.Add("@email", userAdmin.Email);
            parameters.Add("@created_at", userAdmin.CreatedAt);
            parameters.Add("@updated_at", userAdmin.UpdatedAt);
            parameters.Add("@flag", userAdmin.Flag);
            parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output); // Output parameter

            // Execute stored procedure
          _connection.Execute("InsertMasterAdmin", parameters);

            // Retrieve output parameter value
            int generatedId = parameters.Get<int>("@id");
            return generatedId;
        }

        public int UpdateUserAdmin(UserAdmin userAdmin)
        {
            throw new NotImplementedException();
        }
    }
}

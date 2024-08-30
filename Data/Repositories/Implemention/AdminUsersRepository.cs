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
    public class AdminUsersRepository : IAdminUsers
    {
        public readonly DbConnection _connection;
        public AdminUsersRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<List<AdminUser>> GetAdminUsersAsync()
        {
            List<AdminUser> adminUsers = (List<AdminUser>)await _connection.QueryAsync<AdminUser>("GetAllAdminUsers", commandType: CommandType.StoredProcedure);
            return adminUsers;
        }
         public async Task<int> DeleteAdminUserAsync(int Id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@ID_SERIAL", Id);
            dynamicParameters.Add("@DeletedID", DbType.Int32, direction: ParameterDirection.Output);
            int ID = await _connection.ExecuteAsync("DeleteAdminUser", dynamicParameters);
            ID = dynamicParameters.Get<Int32>("@DeletedID");
            return ID;
        }

        public async Task<AdminUser> GetAdminUserByIdAsync(int Id)
        {
            AdminUser adminUser = await _connection.QueryFirstAsync<AdminUser>("GetAdminUserByID", new { @ID_SERIAL = Id });
            return adminUser;
        }

        

       public async Task<int> InsertAdminUser(AdminUser adminUser)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Username", adminUser.Username);
            dynamicParameters.Add("@Password", adminUser.Password);
            dynamicParameters.Add("@Email", adminUser.Email);
            dynamicParameters.Add("@NewID", DbType.Int32, direction: ParameterDirection.Output);
            await _connection.ExecuteAsync("InsertAdminUser", dynamicParameters);
            int id = dynamicParameters.Get<int>("@NewID");
            return id;
        }

        public async Task<int> UpdateAdminUserAsync(AdminUser adminUser)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@ID_SERIAL", adminUser.ID_SERIAL);
            dynamicParameters.Add("@Username", adminUser.Username);
            dynamicParameters.Add("@Password", adminUser.Password);
            dynamicParameters.Add("@Email", adminUser.Email);
            dynamicParameters.Add("@UpdatedID", DbType.Int32, direction: ParameterDirection.Output);
            await _connection.ExecuteAsync("UpdateAdminUser", dynamicParameters);
            int id = dynamicParameters.Get<int>("@UpdatedID");
            return id;
        }
    }
}

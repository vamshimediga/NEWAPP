using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Data.Repositories.Implemention
{
    public class AccessTableRepository : IAccessTable
    {
        public readonly IDbConnection _connection;

        public AccessTableRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public  async  Task<int> delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AccessID", id, DbType.Int32);
            await _connection.ExecuteAsync("[dbo].[DeleteAccessRecord]", parameters);
            return id;
        }

        public async Task<AccessTable> GetAccessTableByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AccessID", id);
            AccessTable accessTable = await _connection.QuerySingleAsync<AccessTable>("[dbo].[GetAccessByAccessID]", parameters);
            return accessTable;
        }

        public async Task<List<AccessTable>> GetAllAccessTablesAsync()
        {
            List<AccessTable> accessTables = (List<AccessTable>)await _connection.QueryAsync<AccessTable>("sp_GetAllAccessRecords");
            return accessTables;
        }

        public async Task<int> insert(AccessTable accessTable)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", accessTable.UserID);
            parameters.Add("@PermissionLevel", accessTable.PermissionLevel);
            parameters.Add("@Resource", accessTable.Resource);
            parameters.Add("@GrantedDate", accessTable.GrantedDate);
            parameters.Add("@IsActive", accessTable.IsActive);
            parameters.Add("@NewAccessID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            await _connection.ExecuteAsync("InsertAccessRecord", parameters);

            // Retrieve the output parameter (newly inserted AccessID)
            int newAccessID = parameters.Get<int>("@NewAccessID");
            return newAccessID;
        }

        public async Task<int> update(AccessTable accessTable)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AccessID", accessTable.AccessID, DbType.Int32);
            parameters.Add("@UserID", accessTable.UserID);
            parameters.Add("@PermissionLevel", accessTable.PermissionLevel, DbType.String);
            parameters.Add("@Resource", accessTable.Resource, DbType.String);
            parameters.Add("@GrantedDate", accessTable.GrantedDate, DbType.Date);
            parameters.Add("@IsActive", accessTable.IsActive, DbType.Boolean);

            // Output parameter to capture the updated AccessID
            parameters.Add("@UpdatedAccessID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            await _connection.ExecuteAsync("[dbo].[UpdateAccessTable]", parameters);

            // Retrieve the value of the output parameter
            int updatedAccessID = parameters.Get<int>("@UpdatedAccessID");

            return updatedAccessID;
        }
    }
}

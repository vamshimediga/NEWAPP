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

        public Task<int> delete(int id)
        {
            throw new NotImplementedException();
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

        public Task<int> update(AccessTable accessTable)
        {
            throw new NotImplementedException();
        }
    }
}

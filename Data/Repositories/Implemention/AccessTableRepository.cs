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
    }
}

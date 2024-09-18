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
    public class LeadSourceRepository : ILeadSource
    {
        public readonly DbConnection _connection;
        public LeadSourceRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<List<LeadSource>> GetleadSources()
        {
            List<LeadSource> leadSources =(List<LeadSource>) await _connection.QueryAsync<LeadSource>("GETLeadSource");
            return leadSources;
        }

        public async Task<int> Insert(LeadSource leadSource)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SourceName", leadSource.SourceName);
            parameters.Add("@Description", leadSource.Description);
            parameters.Add("@CreatedDate", leadSource.CreatedDate);
            parameters.Add("@ModifiedDate", leadSource.ModifiedDate);
            parameters.Add("@LeadSourceID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            await _connection.ExecuteAsync("InsertLeadSource", parameters);

            // Retrieve the output value for LeadSourceID
            int leadSourceID = parameters.Get<int>("@LeadSourceID");

            return leadSourceID;
        }
    }
}

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

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> DeleteAsync(int id)
        //{
        //    string ids = string.Join(",", id);
        //    DynamicParameters dynamicParameters = new DynamicParameters();
        //    dynamicParameters.Add("@LeadsourceToDelete", ids);
        //    int affectedRows = await _connection.ExecuteAsync("DeleteMultipleLeadsource", dynamicParameters);
        //    return true;
        //}

        //public async Task<bool> DeleteMultipleAsync(List<int> ids)
        //{
        //    string idss = "";
        //    foreach (int id in ids)
        //    {
        //        idss = string.Join(",", ids);
        //    }
        //    DynamicParameters dynamicParameters = new DynamicParameters();
        //    dynamicParameters.Add("@LeadsourceToDelete", idss);
        //    int affectedRows = await _connection.ExecuteAsync("DeleteMultipleLeadsource", dynamicParameters);
        //    return true;
        //}

        public async Task<LeadSource> GetById(int id)
        {
            LeadSource leadSource= await _connection.QueryFirstOrDefaultAsync<LeadSource>("GetLeadSourceById", new { @LeadSourceID=id});
            return leadSource;
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

        public async Task<int> Update(LeadSource leadSource)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeadSourceID", leadSource.LeadSourceID);
            parameters.Add("@SourceName", leadSource.SourceName);
            parameters.Add("@Description", leadSource.Description);
            parameters.Add("@ModifiedDate", leadSource.ModifiedDate);

            // Output parameter
            parameters.Add("@UpdatedID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            await  _connection.ExecuteAsync("UpdateLeadSource", parameters);

            // Get the value of the output parameter
            int updatedID = parameters.Get<int>("@UpdatedID");

            return updatedID; // Return the updated LeadSourceID
        }
    }
}

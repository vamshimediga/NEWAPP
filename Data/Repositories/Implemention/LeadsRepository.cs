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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class LeadsRepository : ILeads
    {
        public IDbConnection _connection;
        public LeadsRepository(IConfiguration configuration) {

            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public int delete(string[] leadIds)
        {
            string leadIdsString = string.Join(",", leadIds);
            int ids=_connection.Execute("DeleteMultipleLeads", new { RecordIds = leadIdsString });
            return ids;
        }

        public int insert(Lead lead)
        {
            // Define the parameters to be passed to the stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", lead.FirstName);
            parameters.Add("@LastName", lead.LastName);
            parameters.Add("@Email",lead.Email);
            parameters.Add("@PhoneNumber",lead.PhoneNumber);
            parameters.Add("@LeadSource",lead.LeadSource);
            parameters.Add("@Status",lead.Status);
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@ModifiedDate", DateTime.Now);

            // Define the output parameter
            parameters.Add("@LeadID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            _connection.Execute("InsertLead", parameters);

            // Retrieve the output parameter value
            int newLeadID = parameters.Get<int>("@LeadID");
            return newLeadID;
        }

        public Lead LeadById(int id)
        {
            Lead lead = _connection.QueryFirstOrDefault<Lead>("SELECT * FROM Leads WHERE LeadID = @LeadID",new { LeadID = id });
            return lead;
        }

        public List<Lead> Leads()
        {
            List<Lead> leads = _connection.Query<Lead>("SELECT * FROM Leads").ToList();

            // Sort the leads list by FirstName in ascending order
            var sortedLeads = from lead in leads
                              orderby lead.FirstName ascending
                              select lead;

            // Convert the sorted result to a List<Lead>
            return sortedLeads.ToList();
        }

        public int update(Lead lead)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeadID", lead.LeadID, DbType.Int32);
            parameters.Add("@FirstName", lead.FirstName, DbType.String);
            parameters.Add("@LastName", lead.LastName, DbType.String);
            parameters.Add("@Email", lead.Email, DbType.String);
            parameters.Add("@PhoneNumber", lead.PhoneNumber, DbType.String);
            parameters.Add("@LeadSource", lead.LeadSource, DbType.String);
            parameters.Add("@Status", lead.Status, DbType.String);
            parameters.Add("@ModifiedDate", lead.ModifiedDate, DbType.DateTime);
            _connection.Execute("UpdateLead", parameters);
            return parameters.Get<int>("@LeadID");
        }
    }
}

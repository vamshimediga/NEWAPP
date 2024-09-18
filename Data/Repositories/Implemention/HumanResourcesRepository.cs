using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class HumanResourcesRepository : IHumanResources
    {
        public readonly IDbConnection _dbconnection;
        public HumanResourcesRepository(IConfiguration configuration)
        {
            _dbconnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<List<HumanResources>> GetHumanResources()
        {
            List<HumanResources> humanResources = (List<HumanResources>)await _dbconnection.QueryAsync<HumanResources>("GetAllHumanResources");
            return humanResources;
        }

        public async Task<HumanResources> GetHumanResourcesById(int Id)
        {
            HumanResources humanResources = await  _dbconnection.QueryFirstOrDefaultAsync<HumanResources>("sp_GetEmployeeByID", new { @EmployeeID = Id });
            return humanResources;
        }

        public async Task<int> Insert(HumanResources humanResources)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", humanResources.FirstName, DbType.String);
            parameters.Add("@LastName", humanResources.LastName, DbType.String);
            parameters.Add("@Email", humanResources.Email, DbType.String);
            parameters.Add("@HireDate", humanResources.HireDate, DbType.Date);
            parameters.Add("@NewEmployeeID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            // Execute stored procedure
           await  _dbconnection.ExecuteAsync("InsertHumanResources", parameters, commandType: CommandType.StoredProcedure);

            // Retrieve the output parameter value
            return parameters.Get<int>("@NewEmployeeID");
        }

        public async Task<int> Update(HumanResources humanResources)
        {

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", humanResources.EmployeeID);
                parameters.Add("@FirstName", humanResources.FirstName, DbType.String);
                parameters.Add("@LastName", humanResources.LastName, DbType.String);
                parameters.Add("@Email", humanResources.Email, DbType.String);
                parameters.Add("@HireDate", humanResources.HireDate, DbType.Date);
                parameters.Add("@UpdatedEmployeeID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await _dbconnection.ExecuteAsync("UpdateHumanResources", parameters);
                int Id = parameters.Get<int>("@UpdatedEmployeeID");
                return Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

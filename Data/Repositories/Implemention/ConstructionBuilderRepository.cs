using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Data.Repositories.Implemention
{
    public class ConstructionBuilderRepository : IConstructionBuilder
    {
        public readonly IDbConnection _connection;

        public ConstructionBuilderRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task Delete(int cid)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BuilderID", cid);
            await _connection.ExecuteAsync("DeleteConstructionBuilder", parameters);
        }

        public async Task<List<ConstructionBuilder>> Get()
        {
            List<ConstructionBuilder> list = (List<ConstructionBuilder>)await _connection.QueryAsync<ConstructionBuilder>("sp_GetAllConstructionBuilders");
            return list;
        }

        public async Task<ConstructionBuilder> GetConstructorById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BuilderID", id);
            ConstructionBuilder constructorBuilder = await _connection.QueryFirstOrDefaultAsync<ConstructionBuilder>("[dbo].[GetConstructionBuilderByID]", parameters);
            return constructorBuilder;
        }

        public async Task Insert(ConstructionBuilder builder)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BuilderName", builder.BuilderName, DbType.String, ParameterDirection.Input, 100);
            parameters.Add("@CompanyName", builder.CompanyName, DbType.String, ParameterDirection.Input, 100);
            parameters.Add("@ContactNumber", builder.ContactNumber, DbType.String, ParameterDirection.Input, 15);
            await _connection.ExecuteAsync("InsertConstructionBuilder", parameters);
        }

        public async Task Update(ConstructionBuilder builder)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BuilderID", builder.BuilderID);
            parameters.Add("@BuilderName", builder.BuilderName);
            parameters.Add("@CompanyName", builder.CompanyName);
            parameters.Add("@ContactNumber", builder.ContactNumber);

            string sql = "UpdateConstructionBuilder"; // Stored Procedure Name

            await _connection.ExecuteAsync(sql, parameters);
        }
    }
}

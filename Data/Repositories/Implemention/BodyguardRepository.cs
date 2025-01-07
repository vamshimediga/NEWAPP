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
using System.Security.Cryptography;
using System.Net.NetworkInformation;

namespace Data.Repositories.Implemention
{
    public class BodyguardRepository : IBodyguard
    {

        public readonly IDbConnection _connection;

        public BodyguardRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BodyguardID", id, DbType.Int32);
            parameters.Add("@DeletedBodyguardID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            await _connection.ExecuteAsync("DeleteBodyguard", parameters, commandType: CommandType.StoredProcedure);

            // Retrieve the output parameter value
            int did= parameters.Get<int>("@DeletedBodyguardID");
            return did;
        }

        public async Task<List<Bodyguard>> Get()
        {

            List<Bodyguard> bodyguards = (List<Bodyguard>)await _connection.QueryAsync<Bodyguard>("GetAllBodyguards", commandType: CommandType.StoredProcedure);
            return bodyguards;
        }

        public async Task<Bodyguard> GetByid(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BodyguardID", id, DbType.Int32);
            Bodyguard bodyguard = _connection.QueryFirstOrDefault<Bodyguard>("GetBodyguardByID", parameters,commandType: CommandType.StoredProcedure);
            return bodyguard;
        }

        public async Task<int> insert(Bodyguard bodyguard)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", bodyguard.FirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("@LastName", bodyguard.LastName, DbType.String, ParameterDirection.Input);
            parameters.Add("@PhoneNumber", bodyguard.PhoneNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@Status", bodyguard.Status, DbType.String, ParameterDirection.Input);
            parameters.Add("@NewBodyguardID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
           await  _connection.ExecuteAsync("InsertBodyguard", parameters, commandType: CommandType.StoredProcedure);

            // Get the output parameter value
            int id = parameters.Get<int>("@NewBodyguardID");
            return id;
        }

        public async Task<int> update(Bodyguard bodyguard)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BodyguardID", bodyguard.BodyguardID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@FirstName", bodyguard.FirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("@LastName", bodyguard.LastName, DbType.String, ParameterDirection.Input);
            parameters.Add("@PhoneNumber", bodyguard.PhoneNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@Status", bodyguard.Status, DbType.String, ParameterDirection.Input);
            parameters.Add("@UpdatedBodyguardID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
           await  _connection.ExecuteAsync("UpdateBodyguard", parameters, commandType: CommandType.StoredProcedure);

            // Get the output parameter value
            int ID= parameters.Get<int>("@UpdatedBodyguardID");
            return ID;
        }
    

      
    }
}

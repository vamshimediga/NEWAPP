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
using static Dapper.SqlMapper;

namespace Data.Repositories.Implemention
{
    public class ClientRepository : IClient
    {
        private readonly DbConnection _dbContext;
        private readonly IConfiguration _config;
        private readonly string ConnectionStrings = "DefaultConnection";

        public ClientRepository(IConfiguration config)
        {
            _config = config;
        }

        private DbConnection GetDbConnection()
        {
            return new SqlConnection(_config.GetConnectionString(ConnectionStrings));
        }
        public async Task<List<Client>> GetClients()
        {

            try
            {
                DbConnection dbConnection = GetDbConnection();
                await dbConnection.OpenAsync();
                List<Client> clients = (List<Client>)await dbConnection.QueryAsync<Client>("GetAllClients", commandType: CommandType.StoredProcedure);
                dbConnection.Close();
                return clients;
            }
            catch (Exception)
            {

                throw;
            }
           
            
        }

        public async Task<int> InsertClient(Client client)
        {
            DbConnection dbConnection = GetDbConnection();
            await dbConnection.OpenAsync();
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@FirstName", client.FirstName);
            dynamicParameters.Add("@LastName", client.LastName);
            dynamicParameters.Add("@Email", client.Email);
            dynamicParameters.Add("@Phone", client.Phone);
            dynamicParameters.Add("@Address", client.Address);
            dynamicParameters.Add("@City", client.City);
            dynamicParameters.Add("@State", client.State);
            dynamicParameters.Add("@ZipCode", client.ZipCode);
            dynamicParameters.Add("@Country", client.Country);
            dynamicParameters.Add("@InsertedID", DbType.Int32, direction: ParameterDirection.Output);
            var result = await dbConnection.ExecuteAsync("InsertClient", dynamicParameters, commandType: CommandType.StoredProcedure);
            int Id = dynamicParameters.Get<int>("@InsertedID");
            dbConnection.Close();
            return Id;
        }

        public async Task<bool> DeleteClients(int[] ids)
        {
            DbConnection dbConnection = GetDbConnection();
            await dbConnection.OpenAsync();
            var result = await dbConnection.ExecuteAsync("DELETE FROM Client WHERE ClientID IN @ClientID", new { ClientID = ids }, commandType:CommandType.Text);
            bool isSuccess = result > 0;
            dbConnection.Close();
            return isSuccess;

        }

        public async Task<Client> GetClientById(int id)
        {
            DbConnection dbConnection = GetDbConnection();
            await dbConnection.OpenAsync();
            Client client = await dbConnection.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Client WHERE ClientID=@ClientID", new { @ClientID = id }, commandType: CommandType.Text);
            dbConnection.Close();
            return client;
        }

        public async Task<int> UpdateClientDetails(Client client)
        {

            DbConnection dbConnection = GetDbConnection();
            await dbConnection.OpenAsync();
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@ClientID",client.ClientID);
            dynamicParameters.Add("@FirstName", client.FirstName);
            dynamicParameters.Add("@LastName", client.LastName);
            dynamicParameters.Add("@Email", client.Email);
            dynamicParameters.Add("@Phone", client.Phone);
            dynamicParameters.Add("@Address", client.Address);
            dynamicParameters.Add("@City", client.City);
            dynamicParameters.Add("@State", client.State);
            dynamicParameters.Add("@ZipCode", client.ZipCode);
            dynamicParameters.Add("@Country", client.Country);
            dynamicParameters.Add("@UpdatedClientID", DbType.Int32, direction: ParameterDirection.Output);
            var result = await dbConnection.ExecuteAsync("UpdateClient", dynamicParameters, commandType: CommandType.StoredProcedure);
            int Id = dynamicParameters.Get<int>("@UpdatedClientID");
            dbConnection.Close();
            return Id;
            
        }
    }
}

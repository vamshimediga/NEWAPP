using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories.Interfaces;
using DomainModels;
using Dapper;

namespace Data.Repositories.Implemention
{
    public class PsplClientRepository:IPsplClient
    {
        public readonly IDbConnection _connection;

        public PsplClientRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PsplClient>> GetAll()
        {
            List<PsplClient> psplClients = (List<PsplClient>)await _connection.QueryAsync<PsplClient>("[dbo].[GetPsplClients]");
            return psplClients;
        }

        public Task<PsplClient> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(PsplClient psplClient)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(PsplClient psplClient)
        {
            throw new NotImplementedException();
        }
    }
}

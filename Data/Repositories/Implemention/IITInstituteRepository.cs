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
    public class IITInstituteRepository : IITInstitute
    {

        public readonly IDbConnection _connection;

        public IITInstituteRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ITInstitute> GetById(int id)
        {
            ITInstitute iTInstitute = await _connection.QueryFirstOrDefaultAsync<ITInstitute>("GetITInstituteById", new { InstituteID = id });
            return iTInstitute;
        }

        public async Task<List<ITInstitute>> GetITInstitutes()
        {
            List<ITInstitute> iTInstitutes = (List<ITInstitute>)await _connection.QueryAsync<ITInstitute>("GetAllITInstitutes");
            return iTInstitutes;
        }

        public Task<int> insert(ITInstitute institute)
        {
            throw new NotImplementedException();
        }

        public Task<int> update(ITInstitute institute)
        {
            throw new NotImplementedException();
        }
    }
}

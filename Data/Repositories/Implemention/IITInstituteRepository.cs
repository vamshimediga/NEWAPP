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

        public async Task<int> update(ITInstitute institute)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@InstituteID", institute.InstituteID);  // Input parameter
            parameters.Add("@InstituteName", institute.InstituteName);  // Input parameter
            parameters.Add("@Location", institute.Location);  // Input parameter
            parameters.Add("@ContactNumber", institute.ContactNumber);  // Input parameter
            parameters.Add("@EstablishedYear", institute.EstablishedYear);  // Input parameter
            parameters.Add("@CoursesOffered", institute.CoursesOffered);  // Input parameter
            parameters.Add("@Rating", institute.Rating);  // Input parameter
            parameters.Add("@ModifiedDate", institute.ModifiedDate);  // Input parameter
            parameters.Add("@OutputInstituteID", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter
            await  _connection.ExecuteAsync("dbo.UpdateITInstitute", parameters);
            var outputInstituteID = parameters.Get<int>("@OutputInstituteID");
            return outputInstituteID;
        }
    }
}

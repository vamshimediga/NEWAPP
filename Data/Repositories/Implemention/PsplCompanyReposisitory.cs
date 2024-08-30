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

namespace Data.Repositories.Implemention
{
    public class PsplCompanyReposisitory : IPsplCompany
    {

        public readonly IDbConnection _connection;
        public PsplCompanyReposisitory(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<PsplCompany> GetPsplCompanies()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "SELECT");

            List<PsplCompany> companies =_connection.Query<PsplCompany>("[dbo].[ManagePsplCompany]", parameters).ToList();

            return companies;
        }

        public PsplCompany GetPsplCompany(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "GetById");
            parameters.Add("@CompanyID", id);
            PsplCompany companies = _connection.QueryFirstOrDefault<PsplCompany>("[dbo].[ManagePsplCompany]", parameters);
            return companies;
        }

        public int Insert(PsplCompany psplCompany)
        {
            throw new NotImplementedException();
        }

        public int update(PsplCompany psplCompany)
        {
            throw new NotImplementedException();
        }
    }
}

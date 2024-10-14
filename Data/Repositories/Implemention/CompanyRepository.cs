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
    public class CompanyRepository : ICompany
    {
        public readonly IDbConnection _connection;

        public CompanyRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<List<Company>> GetAll()
        {
            string sqlQuery = "SELECT CompanyID, CompanyName, City, PhoneNumber, IsActive FROM Company";
            IEnumerable<Company> companies = await  _connection.QueryAsync<Company>(sqlQuery);

            List<Company> orderedCompanies = (from c in companies
                                              orderby c.CompanyName
                                              select c).ToList();

            return orderedCompanies;
        }
    }
}

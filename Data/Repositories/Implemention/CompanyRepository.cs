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
using System.ComponentModel.Design;

namespace Data.Repositories.Implemention
{
    public class CompanyRepository : ICompany
    {
        public readonly IDbConnection _connection;

        public CompanyRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Task<int> delete(int id)
        {
            throw new NotImplementedException();
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

        public async Task<Company> GetById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyID", id);

            // QuerySingleOrDefault calls the stored procedure and maps the result to the Company class
            return  _connection.QuerySingleOrDefault<Company>("GetCompanyByID", parameters, commandType: CommandType.StoredProcedure);
        }

        public Task<int> insert(Company company)
        {
            throw new NotImplementedException();
        }

        public Task<int> update(Company company)
        {
            throw new NotImplementedException();
        }
    }
}

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

        public async Task<bool> delete(List<int> id)
        {
            string companyIDsString = string.Join(",", id);

            // Call the stored procedure to delete the companies
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyIDs", companyIDsString);
            await _connection.ExecuteAsync("[dbo].[DeleteCompanies]", parameters);
            return true;
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

        public async Task<int> insert(Company company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyName", company.CompanyName);
            parameters.Add("@City", company.City);
            parameters.Add("@PhoneNumber", company.PhoneNumber);
            parameters.Add("@IsActive", company.IsActive);

            // Output parameter for the inserted CompanyID
            parameters.Add("@InsertedID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure using Dapper
            await _connection.ExecuteAsync("InsertCompany", parameters);

            // Retrieve the output parameter value
            int insertedCompanyId = parameters.Get<int>("@InsertedID");

            return insertedCompanyId;
        }

        public async Task<int> update(Company company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyID", company.CompanyID, DbType.Int32);
            parameters.Add("@CompanyName", company.CompanyName, DbType.String);
            parameters.Add("@City", company.City, DbType.String);
            parameters.Add("@PhoneNumber", company.PhoneNumber, DbType.String);
            parameters.Add("@IsActive", company.IsActive, DbType.Boolean);

            // Execute stored procedure and get the updated CompanyID
            int updatedCompanyId =await _connection.ExecuteAsync("[dbo].[UpdateCompany]",parameters);

            return updatedCompanyId;
        }
    }
}

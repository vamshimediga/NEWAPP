using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class online_retailUserLoginRepository : Ionline_retailUserLogin
    {
        public readonly IDbConnection _dbConnection;
        public online_retailUserLoginRepository(IConfiguration configuration) {
        
        _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Task<bool> Delet(int id)
        {
            throw new NotImplementedException();
        }

        public Task<online_retailUserLogin> GetOnline_RetailUserLogin(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<online_retailUserLogin>> GetOnline_RetailUserLogins()
        {
            List<online_retailUserLogin> online_RetailUserLogins = (List<online_retailUserLogin>)await _dbConnection.QueryAsync<online_retailUserLogin>("GetAllonline_retail_appUserLogins");
            return online_RetailUserLogins;
        }

        public Task<int> insert(online_retailUserLogin online_retailUserLogin)
        {
            throw new NotImplementedException();
        }

        public Task<int> update(online_retailUserLogin online_retailUserLogin)
        {
            throw new NotImplementedException();
        }
    }
}

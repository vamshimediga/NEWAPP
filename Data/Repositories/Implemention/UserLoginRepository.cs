using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class UserLoginRepository : IUserLogin
    {
        public readonly DbConnection _connection;
        public UserLoginRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<List<UserLogin>> UserLogins()
        {
            return (List<UserLogin>)await _connection.QueryAsync<UserLogin>("[railway_system].[GetTopUsers]");
        }
    }
}

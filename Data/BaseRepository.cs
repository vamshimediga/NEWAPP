using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BaseRepository: IDisposable
    {
        protected readonly DbConnection _connection;

        public BaseRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        protected async Task<List<T>> QueryAsync<T>(string storedProcedure, object parameters = null)
        {
            return (List<T>)await _connection.QueryAsync<T>(storedProcedure, parameters);
        }

        protected async Task<T> QueryFirstAsync<T>(string storedProcedure, object parameters = null)
        {
            return await _connection.QueryFirstAsync<T>(storedProcedure, parameters);
        }

        protected async Task<int> ExecuteAsync(string storedProcedure, object parameters = null)
        {
            return await _connection.ExecuteAsync(storedProcedure, parameters);
        }
        protected async Task<T> QueryFirstOrDefaultAsync<T>(string storedProcedure, object parameters = null)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters);
        }

        //GarbageCollection 
        //DataBase Disconnection
        //Deallocation of Memory
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}

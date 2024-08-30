using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class StockoverflowRepository : IStockoverflow
    {
        public readonly IDbConnection _connection;

        public StockoverflowRepository(IConfiguration configuration) {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public int DeleteStockoverflow(int id)
        {
            throw new NotImplementedException();
        }

        public Stockoverflow GetStockoverflowById(int id)
        {
            Stockoverflow stockoverflow = _connection.QueryFirstOrDefault<Stockoverflow>("GETStockoverflowByID", new { @STACKID = id });
            return stockoverflow;
        }

        public List<Stockoverflow> GetStockoverflows()
        {
            
            List<Stockoverflow> stockoverflows = _connection.Query<Stockoverflow>("GETStockoverflow").ToList(); 
            return stockoverflows;
        }

        public int insertStockoverflow(Stockoverflow stockoverflow)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StackName", stockoverflow.StackName);
            parameters.Add("@StackEmail", stockoverflow.StackEmail);
            parameters.Add("@StackPhone", stockoverflow.StackPhone);
            parameters.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute("InsertStockoverflow", parameters, commandType: CommandType.StoredProcedure);
            int id = parameters.Get<int>("@ID");
            return id;
        }

        public Stockoverflow IsEmailExistsAsync(string email)
        {
            Stockoverflow stockoverflow = _connection.QueryFirstOrDefault<Stockoverflow>("CheckEmailExists", new { @Email = email });
            return stockoverflow;
        }

        public int updateStockoverflow(Stockoverflow stockoverflow)
        {
            throw new NotImplementedException();
        }
    }
}

using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class ProductChartRepository : IProductChart
    {
        public readonly IDbConnection _connection;
        public ProductChartRepository(IConfiguration configuration) {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public List<ProductChart> GetProductCharts()
        {
            List<ProductChart> productCharts = _connection.Query<ProductChart>("SELECT * FROM ProductSales").ToList();
            return productCharts;   
        }
    }
}

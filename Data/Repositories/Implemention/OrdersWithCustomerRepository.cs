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
    public class OrdersWithCustomerRepository : IOrdersWithCustomer
    {
        public readonly IDbConnection _dbConnection;
        public OrdersWithCustomerRepository(IConfiguration configuration) {

            _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public int delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrdersWithCustomer> GetAll()
        {
            List<OrdersWithCustomer> ordersWithCustomers = _dbConnection.Query<OrdersWithCustomer>("GetOrdersWithCustomerInfo").ToList();
            return ordersWithCustomers;
        }

        public OrdersWithCustomer GetById(int id)
        {
            OrdersWithCustomer ordersWithCustomer = _dbConnection.QueryFirstOrDefault<OrdersWithCustomer>("GetOrderById", new { @OrderID = id });
            return ordersWithCustomer;
        }

        public int insert(OrdersWithCustomer order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderID", order.OrderID);
            parameters.Add("@OrderDate", order.OrderDate);
            parameters.Add("@CustomerID", order.CustomerID);
            parameters.Add("@NewOrderID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dbConnection.Execute("InsertOrder", parameters);
            return parameters.Get<int>("@NewOrderID");
        }

        public int update(OrdersWithCustomer customer)
        {
            throw new NotImplementedException();
        }
    }
}

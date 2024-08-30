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
    public class RestaurantRepository : IRestaurant
    {
        public readonly IDbConnection _connection=null;
        public RestaurantRepository(IConfiguration configuration) {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public List<Restaurant> GetAll()
        {
            List<Restaurant> restaurants = _connection.Query<Restaurant>("ListRestaurants").ToList();
            return restaurants;
        }

        public int insert(Restaurant restaurant)
        {
            // Parameters for the stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@RestaurantID", restaurant.RestaurantID, DbType.Int32);
            parameters.Add("@RestaurantName", restaurant.RestaurantName, DbType.String, size: 100);
            parameters.Add("@Address", restaurant.Address, DbType.String, size: 200);
            parameters.Add("@City", restaurant.City, DbType.String, size: 100);
            parameters.Add("@State", restaurant.State, DbType.String, size: 50);
            parameters.Add("@ZipCode",restaurant.ZipCode, DbType.String, size: 20);
            parameters.Add("@Phone", restaurant.Phone, DbType.String, size: 20);
            // Define the output parameter
            parameters.Add("@NewRestaurantID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute("InsertRestaurant", parameters);
            int Id = parameters.Get<int>("@NewRestaurantID");
            return Id;
        }
    }
}

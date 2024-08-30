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
    public class UserradiochkdropRepository : IUserradiochkdrop
    {
        public readonly IDbConnection _dbConnection;
        public UserradiochkdropRepository(IConfiguration configuration) {
            _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        
        }
        public List<Userchkradio> GetUserchkradio()
        {
           List<Userchkradio> userchkradios = _dbConnection.Query<Userchkradio>("SELECT * FROM Userradio_chk_drop").ToList();
           return userchkradios;
        }

        public int InsertUserchkradio(Userchkradio userchkradio)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userchkradio.UserID);
            parameters.Add("@Gender", userchkradio.Gender);
            parameters.Add("@EmailNotifications", userchkradio.EmailNotifications);
            parameters.Add("@CountryID", userchkradio.CountryID);
            parameters.Add("@CountryName", userchkradio.CountryName);
            parameters.Add("@InsertedID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            _dbConnection.Execute("InsertUserRadioData", parameters);

            // Retrieve the value of the output parameter
            return parameters.Get<int>("@InsertedID");
        }
    }
}

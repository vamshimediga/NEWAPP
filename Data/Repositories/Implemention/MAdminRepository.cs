using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class MAdminRepository : IMAdmin
    {
        public readonly IDbConnection _connection;
        public MAdminRepository(IConfiguration configuration) {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public MAdmin GetMAdminByid(int id)
        {
            MAdmin mAdmin = _connection.QueryFirstOrDefault<MAdmin>("GetMasterAdminById", new { @Id = id });
            return mAdmin;
        }

        public List<MAdmin> GetMAdmins()
        {
            List<MAdmin> mAdmins = _connection.Query<MAdmin>("GetMasterAdmins").ToList();
            return mAdmins; 
        }

        public int Insert(MAdmin madmin)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@username", madmin.Username);
            parameters.Add("@password", madmin.Password);
            parameters.Add("@email", madmin.Email);
            parameters.Add("@created_at", madmin.CreatedAt);
            parameters.Add("@updated_at", madmin.UpdatedAt);
            parameters.Add("@flag", madmin.Flag);
            parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute("InsertUserAdmin", parameters);


            var parameters1 = new DynamicParameters();
            parameters1.Add("@id", madmin.Id);
            parameters1.Add("@username", madmin.Username);
            parameters1.Add("@password", madmin.Password);
            parameters1.Add("@email", madmin.Email);
            parameters1.Add("@created_at", madmin.CreatedAt);
            parameters1.Add("@updated_at", madmin.UpdatedAt);
            parameters1.Add("@flag", madmin.Flag);

            _connection.Execute("UpdateMasterAdmin", parameters1);
            return parameters.Get<int>("@id");
        }
    }
}

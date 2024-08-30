using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class ManagerRepository : IManager
    {
        public readonly DbConnection _db;
        public ManagerRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public bool Delete(List<int> ids)
        {
            var idls= string.Join(",", ids);
            int Id = _db.Execute("DeleteManagerIds",new { @ManagerIds = idls },commandType:CommandType.StoredProcedure);
            return Id <0;
        }

        public Manager GetManager(int id)
        {
            Manager manager = _db.QueryFirstOrDefault<Manager>("GetManagerById", new { @ManagerID = id }, commandType: CommandType.StoredProcedure);
            return manager;
        }

        public List<Manager> GetManagers()
        {
            List<Manager> list = (List<Manager>)_db.Query<Manager>("GetManagerAndSupervisorData", commandType: CommandType.StoredProcedure);
            return list;
        }

        public int Insert(Manager manager)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ManagerID", manager.ManagerID);
            parameters.Add("@FirstName", manager.FirstName);
            parameters.Add("@LastName", manager.LastName);
            parameters.Add("@Email", manager.Email);
            parameters.Add("@InsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _db.Execute("InsertManager", parameters, commandType: CommandType.StoredProcedure);
            int insertedId = parameters.Get<int>("@InsertedId");
            return insertedId;
        }

        public int Update(Manager manager)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ManagerID", manager.ManagerID);
            parameters.Add("@FirstName", manager.FirstName);
            parameters.Add("@LastName", manager.LastName);
            parameters.Add("@Email", manager.Email);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _db.Execute("UpdateManager", parameters, commandType: CommandType.StoredProcedure);
            int updatedId = parameters.Get<int>("@Id");
            return updatedId;
        }
    }
}

using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class EmployeesRepository:IEmployees
    {

        private readonly DbConnection _dbContext;
        private readonly IConfiguration _config;
        private readonly string ConnectionStrings = "DefaultConnection";
        public EmployeesRepository(IConfiguration config)
        {
            _config = config;
        }
        private DbConnection GetDbConnection()
        {
            return new SqlConnection(_config.GetConnectionString(ConnectionStrings));
        }
        public bool DeleteEmployees(int id)
        {

            DbConnection db = GetDbConnection();
            db.Open();
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeId", id);
            var result = db.ExecuteScalar<Employees>("DeleteEmployee", dynamicParameters, commandType: CommandType.StoredProcedure);
            return result == null ? true : false;
        }

        public List<Employees> GetEmployees()
        {
            DbConnection db = GetDbConnection();
            db.Open();
            var result = db.Query<Employees>("GetAllEmployees", commandType: CommandType.StoredProcedure);
            db.Close();
            return (List<Employees>)result;

        }

        public Employees GetEmployeesByid(int Id)
        {
            DbConnection db = GetDbConnection();
            db.Open();

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", Id);
            var result = db.QueryFirstOrDefault<Employees>("GetEmployeeById", dynamicParameters, commandType: CommandType.StoredProcedure);
            db.Close();
            return result;
        }

        public int InsertEmployees(Employees employees)
        {
            DbConnection db = GetDbConnection();
            db.Open();
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeId", employees.EmployeeId);
            dynamicParameters.Add("@EmployeeName", employees.EmployeeName);
            var result = db.ExecuteScalar<Employees>("InsertEmployee", dynamicParameters, commandType: CommandType.StoredProcedure);
            return result == null ? 1 : 0;

        }

        public int UpdateEmployees(Employees employees)
        {
            DbConnection db = GetDbConnection();
            db.Open();
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeId", employees.EmployeeId);
            dynamicParameters.Add("@NewEmployeeName", employees.EmployeeName);
            var result = db.ExecuteScalar<Employees>("UpdateEmployeeName", dynamicParameters, commandType: CommandType.StoredProcedure);
            return result == null ? 1 : 0;
        }

        

    }
}

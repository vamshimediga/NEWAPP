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
    public class DefectiveProductsRepository : IDefectiveProducts
    {
        public readonly IDbConnection _connection;
        public DefectiveProductsRepository(IConfiguration configuration) {

            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public bool delete(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@ProductID", id);
            dynamicParameters.Add("@DeletedDefectiveProductID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute("DeleteDefectiveProducts", dynamicParameters, commandType: CommandType.StoredProcedure);
            int DeletedId = dynamicParameters.Get<int>("@DeletedDefectiveProductID");
            return DeletedId > 0;
        }

        public List<DefectiveProducts> GetProducts()
        {
            List<DefectiveProducts> defectiveProducts = _connection.Query<DefectiveProducts>("GetDefectiveProducts", commandType: CommandType.StoredProcedure).ToList();
            return defectiveProducts;
        }

        public DefectiveProducts GetProductsById(int id)
        {
            DefectiveProducts defectiveProducts = _connection.QueryFirstOrDefault<DefectiveProducts>("GetDefectiveProductsBYId",new { @DefectiveProductID =id },commandType:CommandType.StoredProcedure);
            return defectiveProducts;
        }

        public int insert(DefectiveProducts defectiveProducts)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", defectiveProducts.ProductID,DbType.Int32, ParameterDirection.Input);
            parameters.Add("@DefectDescription", defectiveProducts.DefectDescription, DbType.String, ParameterDirection.Input);
            parameters.Add("@DefectiveProductID", defectiveProducts.DefectiveProductID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute("InsertDefectiveProduct", parameters, commandType: CommandType.StoredProcedure);
            int defectiveProductID = parameters.Get<int>("@ID");
            return defectiveProductID;
        }

        public int update(DefectiveProducts defectiveProducts)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", defectiveProducts.ProductID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@DefectDescription", defectiveProducts.DefectDescription, DbType.String, ParameterDirection.Input);
            parameters.Add("@DefectiveProductID", defectiveProducts.DefectiveProductID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute("UpdateDefectiveProduct", parameters, commandType: CommandType.StoredProcedure);
            int defectiveProductID = parameters.Get<int>("@ID");
            return defectiveProductID;
        }
    }
}

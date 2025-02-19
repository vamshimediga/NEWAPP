using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace Data.Repositories.Implemention
{
    public class BuildingOwnerRepository : IBuildingOwner
    {
        public readonly IDbConnection _connection;

        public BuildingOwnerRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task Delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OwnerID", id, DbType.Int32);
           await _connection.ExecuteAsync("DeleteBuildingOwner", parameters);
        }

        public async Task<List<BuildingOwner>> GetAll()
        {
            List<BuildingOwner>  buildingOwners=(List<BuildingOwner>)await _connection.QueryAsync<BuildingOwner>("sp_GetAllBuildingOwners");
            return buildingOwners;
        }
    

        public async Task<BuildingOwner> GetById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OwnerID", id, DbType.Int32, ParameterDirection.Input);

            BuildingOwner buildingOwner = await _connection.QueryFirstOrDefaultAsync<BuildingOwner>("GetBuildingOwnerById", parameters);
            return buildingOwner;
        }

        public async Task Insert(BuildingOwner entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("OwnerName", entity.OwnerName, DbType.String);
            parameters.Add("PropertyAddress", entity.PropertyAddress, DbType.String);
            parameters.Add("BuilderID", entity.BuilderID, DbType.Int32);
            await _connection.ExecuteAsync("InsertBuildingOwner", parameters);
        }

        public async Task Update(BuildingOwner entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OwnerID", entity.OwnerID, DbType.Int32);
            parameters.Add("@OwnerName", entity.OwnerName, DbType.String);
            parameters.Add("@PropertyAddress", entity.PropertyAddress, DbType.String);
            parameters.Add("@BuilderID", entity.BuilderID, DbType.Int32);

            string sql = "";

            _connection.Execute("EXEC [dbo].[UpdateBuildingOwner] @OwnerID, @OwnerName, @PropertyAddress, @BuilderID", parameters);
             // Returns true if update is successful
        }
    }
}

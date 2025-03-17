using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class FlatRepository : BaseRepository, IFlat
    {
        public readonly IPropertyOwners _propertyOwners; 
        public FlatRepository(IConfiguration configuration,IPropertyOwners propertyOwners) : base(configuration)
        {
            _propertyOwners = propertyOwners;
        }

        public async Task Delete(int flatID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FlatID", flatID, DbType.Int32);

            await ExecuteAsync("DeleteFlat", parameters);
        }

        public async Task<Flat> GetFlatByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FlatID", id, DbType.Int32, ParameterDirection.Input);
            Flat flat = await QueryFirstOrDefaultAsync<Flat>("[dbo].[GetFlatById]", parameters);
            return flat;
        }

        public async Task<List<Flat>> GetFlatsAsync()
        {
            List<Flat> flats = await QueryAsync<Flat>("[dbo].[GetFlat]");
            // Order by FlatNumber in ascending order
          //  flats = flats.OrderBy(f => f.FlatNumber).ToList();
            foreach (Flat flat in flats)
            {
                flat.PropertyOwner = await _propertyOwners.GetPropertyOwnerById(flat.PropertyOwnerID);
            }
            return flats;
        }

        public async Task Insert(Flat item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FlatID", item.FlatID, DbType.Int32);
            parameters.Add("@PropertyOwnerID", item.PropertyOwnerID, DbType.Int32);
            parameters.Add("@FlatNumber", item.FlatNumber, DbType.String);
            parameters.Add("@Address", item.Address, DbType.String);
            parameters.Add("@Gender", item.Gender, DbType.String);
            parameters.Add("@IsActive", item.IsActive, DbType.Boolean);
            await ExecuteAsync("InsertFlat", parameters);
        }

        public async Task Update(Flat item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FlatID", item.FlatID, DbType.Int32);
            parameters.Add("@PropertyOwnerID", item.PropertyOwnerID, DbType.Int32);
            parameters.Add("@FlatNumber", item.FlatNumber, DbType.String);
            parameters.Add("@Address", item.Address, DbType.String);
            parameters.Add("@Gender", item.Gender, DbType.String);
            parameters.Add("@IsActive", item.IsActive, DbType.Boolean);
            await ExecuteAsync("UpdateFlat", parameters);
        }
    }
}

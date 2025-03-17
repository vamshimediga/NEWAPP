using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class PropertyOwnersRepository :BaseRepository, IPropertyOwners
    {
        public PropertyOwnersRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<PropertyOwner> GetPropertyOwnerById(int propertyOwnerID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PropertyOwnerID", propertyOwnerID, DbType.Int32, ParameterDirection.Input);

            PropertyOwner propertyOwner = await QueryFirstOrDefaultAsync<PropertyOwner>("GetPropertyOwnerByID",parameters);
            return propertyOwner;
        }

        public async Task<List<PropertyOwners>> GetPropertyOwners()
        {
           return await QueryAsync<PropertyOwners>("[dbo].[GetProperty]");
        }
    }
}

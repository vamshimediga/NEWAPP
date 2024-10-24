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
    public class ContectRepository : BaseRepository, IContect
    {
        public ContectRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<int> delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Contect> GetContectById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ContectID", id);
            Contect contect= await QueryFirstOrDefaultAsync<Contect>("GetContectByID",parameters);
            return contect;
        }

        public async Task<List<Contect>> GetContects()
        {
            List<Contect> contects = await QueryAsync<Contect>("[dbo].[GetAllContects]");
            return contects;
        }

        public async Task<int> insert(Contect contect)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", contect.FirstName, DbType.String);
            parameters.Add("@LastName", contect.LastName, DbType.String);
            parameters.Add("@Email", contect.Email, DbType.String);
            parameters.Add("@PhoneNumber", contect.PhoneNumber, DbType.String);
            parameters.Add("@Address", contect.Address, DbType.String);
            parameters.Add("@CreatedDate", contect.CreatedDate, DbType.DateTime);
            parameters.Add("@ModifiedDate", contect.ModifiedDate, DbType.DateTime);

            // Output parameter for the new ContectID
            parameters.Add("@NewContectID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            await  ExecuteAsync("dbo.InsertContect", parameters);

            // Retrieve the new ContectID from the output parameter
            int newContectID = parameters.Get<int>("@NewContectID");

            return newContectID;
        }

        public async Task<IEnumerable<Contect>> SearchContactsAsync(string firstName, string lastName)
        {
            var parameters = new { FirstName = firstName, LastName = lastName };
            IEnumerable<Contect> result = await QueryAsync<Contect>("SearchContectByFirstNameandLastname", parameters);
            return result;
        }

        public async Task<List<Contect>> SearchContectByFirstNameAsync(string firstName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", firstName);
            List<Contect> contects = await QueryAsync<Contect>("[dbo].[SearchContectByFirstName]", parameters);
            return contects.ToList();
        }

        public async Task<int> update(Contect contect)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ContectID", contect.ContectID, DbType.Int32);
            parameters.Add("@FirstName", contect.FirstName, DbType.String);
            parameters.Add("@LastName", contect.LastName, DbType.String);
            parameters.Add("@Email", contect.Email, DbType.String);
            parameters.Add("@PhoneNumber", contect.PhoneNumber, DbType.String);
            parameters.Add("@Address", contect.Address, DbType.String);
            parameters.Add("@ModifiedDate", contect.ModifiedDate, DbType.DateTime);

            // Output parameter for updated ContectID
            parameters.Add("@UpdatedContectID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            await ExecuteAsync("dbo.UpdateContect", parameters);

            // Retrieve the updated ContectID from the output parameter
            int updatedContectID = parameters.Get<int>("@UpdatedContectID");

            return updatedContectID;
        }
    }
}

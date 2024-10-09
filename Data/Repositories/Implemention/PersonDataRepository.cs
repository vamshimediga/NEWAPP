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

namespace Data.Repositories.Implemention
{
    public class PersonDataRepository : IPersonData
    {
        public readonly IDbConnection _connection;
        public PersonDataRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<bool> deletePerson(string[] ids)
        {
            var id=string.Join(",", ids);   
            var parameters = new DynamicParameters();
            parameters.Add("@RecordIds", id);  // Pass the comma-separated list of IDs
            string storedProcedure = "dbo.DeleteMultipleRecordsPersonDataAddressData";
            await _connection.ExecuteAsync(storedProcedure, parameters);
            return true;
        }

       
        public async Task<PersonData> GetPersonById(int personId)
        {
            // SQL query to get person data by PersonID
            string sqlPersonData = @"SELECT p.* FROM PersonData p WHERE p.PersonID = @PersonID";

            // SQL query to get address data by PersonID
            string sqlAddressData = @"SELECT a.* FROM AddressData a WHERE a.PersonID = @PersonID";

            // Fetch the person data using the personId parameter
            PersonData personData = await _connection.QueryFirstOrDefaultAsync<PersonData>(sqlPersonData, new { PersonID = personId });

            if (personData != null)
            {
                // Fetch the address data for the person
                AddressData address = await _connection.QueryFirstOrDefaultAsync<AddressData>(sqlAddressData, new { PersonID = personId });

                // Assign the address to the person if it exists
                if (address != null)
                {
                    personData.Address = address;
                }
            }

            return personData;
        }

        public async Task<List<PersonData>> GetPersonList()
        {
            string sqlPersonData = @"SELECT p.* FROM PersonData p";
            string sqlAddressData = @"SELECT a.* FROM AddressData a";
            List<PersonData> personDatas = (List<PersonData>)await _connection.QueryAsync<PersonData>(sqlPersonData);
            List<AddressData> addresses = (List<AddressData>) await _connection.QueryAsync<AddressData>(sqlAddressData);
            for (int i = 0;i< addresses.Count; i++)
            {
                if (personDatas[i].PersonID == addresses[i].PersonID)
                {
                  personDatas[i].Address = addresses[i];
                }
            }
            return personDatas;
        }

        public Task<int> insertPerson(PersonData person)
        {
            throw new NotImplementedException();
        }

        public Task<int> updatePerson(PersonData person)
        {
            throw new NotImplementedException();
        }
    }
}

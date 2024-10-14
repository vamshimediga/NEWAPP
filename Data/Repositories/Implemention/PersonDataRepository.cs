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
using System.IO;

namespace Data.Repositories.Implemention
{
    public class PersonDataRepository : BaseRepository,IPersonData 
    {
      //  public readonly IDbConnection _connection;
        public PersonDataRepository(IConfiguration configuration):base(configuration)
        {
           // _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<bool> deletePerson(string[] ids)
        {
            var id=string.Join(",", ids);   
            var parameters = new DynamicParameters();
            parameters.Add("@RecordIds", id);  // Pass the comma-separated list of IDs
            string storedProcedure = "dbo.DeleteMultipleRecordsPersonDataAddressData";
            await ExecuteAsync(storedProcedure, parameters);
            return true;
        }

       
        public async Task<PersonData> GetPersonById(int personId)
        {
            // SQL query to get person data by PersonID
            string sqlPersonData = @"SELECT p.* FROM PersonData p WHERE p.PersonID = @PersonID";

            // SQL query to get address data by PersonID
            string sqlAddressData = @"SELECT a.* FROM AddressData a WHERE a.PersonID = @PersonID";

            // Fetch the person data using the personId parameter
            PersonData personData = await QueryFirstOrDefaultAsync<PersonData>(sqlPersonData, new { PersonID = personId });

            if (personData != null)
            {
                // Fetch the address data for the person
                AddressData address = await QueryFirstOrDefaultAsync<AddressData>(sqlAddressData, new { PersonID = personId });

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
            List<PersonData> personDatas = (List<PersonData>)await QueryAsync<PersonData>(sqlPersonData);
            List<AddressData> addresses = (List<AddressData>) await QueryAsync<AddressData>(sqlAddressData);
            for (int i = 0;i< addresses.Count; i++)
            {
                if (personDatas[i].PersonID == addresses[i].PersonID)
                {
                  personDatas[i].Address = addresses[i];
                }
            }
            return personDatas;
        }

        public async Task<int> insertPerson(PersonData person)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PersonID", person.PersonID);
            parameters.Add("@FirstName", person.FirstName);
            parameters.Add("@LastName", person.LastName);
            parameters.Add("@Age", person.Age);
            parameters.Add("@Street", person.Address.Street);
            parameters.Add("@City", person.Address.City);
            parameters.Add("@PostalCode", person.Address.PostalCode);
            parameters.Add("@AddressID", person.Address.AddressID);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            // Execute the stored procedure
            await ExecuteAsync("InsertPersonWithAddressWithoutAutoIncrement", parameters);

            // Get the output value
            int idOutput = parameters.Get<int>("@Id");
            return idOutput;
        }

        public async  Task<int> updatePerson(PersonData person)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PersonID", person.PersonID);
            parameters.Add("@FirstName", person.FirstName);
            parameters.Add("@LastName", person.LastName);
            parameters.Add("@Age", person.Age);
            parameters.Add("@AddressID", person.Address.AddressID);
            parameters.Add("@City", person.Address.City);
            parameters.Add("@Street", person.Address.Street);
            parameters.Add("@PostalCode", person.Address.PostalCode);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await ExecuteAsync("dbo.UpdatePersonWithAddress", parameters);
            // Retrieve the output value
            int updatedPersonId = parameters.Get<int>("@Id");
            return updatedPersonId;
        }
    }
}

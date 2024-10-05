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
        public Task<int> deletePerson(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonData> GetPersonById(int id)
        {
            throw new NotImplementedException();
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

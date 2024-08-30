using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DomainModels;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implemention
{
    public class SampleStudentRepository:ISampleStudent
    {

        public readonly IDbConnection _connection;

        public SampleStudentRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
       

        public List<SampleStudent> GetStudents()
        {
            List<SampleStudent> sampleStudent = _connection.Query<SampleStudent>("SELECT * FROM SampleStudent").ToList();
            return sampleStudent;
        }
    }
}

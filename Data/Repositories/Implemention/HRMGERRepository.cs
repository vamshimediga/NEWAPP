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
    public class HRMGERRepository : IHRMGER
    {

        public readonly IDbConnection _connection;
        public HRMGERRepository(IConfiguration configuration) {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public int DeleteHRMGER(int id)
        {
            throw new NotImplementedException();
        }

        public List<HRMGER> GetHRMGER()
        {
            List<HRMGER> hRMGERs = _connection.Query<HRMGER>("GetAllHR").ToList();
            return hRMGERs;
        }

        public HRMGER GetHRMGERById(int id)
        {
            throw new NotImplementedException();
        }

        public int InsertHRMGER(HRMGER hrmGER)
        {
            throw new NotImplementedException();
        }

        public int UpdateHRMGER(HRMGER hrmGER)
        {
            throw new NotImplementedException();
        }
    }
}

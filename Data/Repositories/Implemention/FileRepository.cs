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
    public class FileRepository : IFile
    {
        public readonly IDbConnection _dbConnection;
        public FileRepository(IConfiguration configuration)
        {
            _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public List<Xlsheet> GetXlsheet()
        {
            List<Xlsheet> xsheetslist = _dbConnection.Query<Xlsheet>("SELECT * FROM xlsheet").ToList();
            return xsheetslist;
        }
    }
}

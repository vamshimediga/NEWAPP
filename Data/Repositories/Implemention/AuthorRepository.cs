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
using static System.Collections.Specialized.BitVector32;

namespace Data.Repositories.Implemention
{
    public class AuthorRepository : IAuthor
    {

        public readonly IDbConnection _connection;

        public AuthorRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Task<int> delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Author>> GetAuthors()
        {
            List<Author> authors = (List<Author>)await _connection.QueryAsync<Author>("GetAuthors");
            return authors;
        }

        public Task<Author> GetByid(int id)
        {
            throw new NotImplementedException();
        }

        public async Task insert(Author author)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AuthorID", author.AuthorID, DbType.Int32);
            parameters.Add("@AuthorName", author.AuthorName, DbType.String);
            await _connection.ExecuteAsync("InsertAuthor", parameters);
        }

        public Task<int> update(Author author)
        {
            throw new NotImplementedException();
        }
    }
}

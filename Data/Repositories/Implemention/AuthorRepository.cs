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
using System.Collections.ObjectModel;

namespace Data.Repositories.Implemention
{
    public class AuthorRepository : IAuthor
    {

        public readonly IDbConnection _connection;

        public AuthorRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<bool> delete(List<string> authorIds)
        {
            string ids = string.Join(",", authorIds);
            var parameters = new DynamicParameters();
            parameters.Add("@AuthorIDs", ids);
            await _connection.ExecuteAsync("dbo.DeleteAuthors", parameters);
            return true;
        }

        public async Task<List<Author>> GetAuthors()
        {
            List<Author> authors = (List<Author>)await _connection.QueryAsync<Author>("GetAuthors");
            return authors;
        }

        public async Task<Author> GetByid(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AuthorID", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            // Execute stored procedure and return the result
            Author author = await _connection.QuerySingleOrDefaultAsync<Author>("dbo.GetAuthorByID", parameters);
            return author;// Assuming the result is a single record or null
        }

        public async Task insert(Author author)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AuthorID", author.AuthorID, DbType.Int32);
            parameters.Add("@AuthorName", author.AuthorName, DbType.String);
            await _connection.ExecuteAsync("InsertAuthor", parameters);
        }

        public async Task update(Author author)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AuthorID", author.AuthorID, DbType.Int32);
            parameters.Add("@AuthorName", author.AuthorName, DbType.String);
            await _connection.ExecuteAsync("[dbo].[UpdateAuthor]", parameters);
        }
    }
}

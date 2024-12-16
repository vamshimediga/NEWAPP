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

namespace Data.Repositories.Implemention
{
    public class Author_USRepository :  IAuthor_US
    {
        private readonly IDbConnection _connection;
        public Author_USRepository(IConfiguration configuration) 
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _connection.Open();
        }

        public Task<int> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Author_US>> GetAll()
        {
            List<Author_US> authors = new List<Author_US>(); // Initialize the list to store the authors

            // Open the SQL connection using the connection from the base class

           // _connection.Open();
            // Use the open connection for the command
            using (SqlCommand cmd = new SqlCommand("GetAllAuthors_US", (SqlConnection)_connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Execute the command and get the reader
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    // Read the data and map it to the Author_US model
                    while (await reader.ReadAsync())
                    {
                        authors.Add(new Author_US()
                        {
                            AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID")),
                            AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                            BirthDate = reader.IsDBNull(reader.GetOrdinal("BirthDate"))
                                        ? (DateTime?)null
                                        : reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                        });
                    }
                }
            }

            return authors; // Return the list of authors
        }


        public Task<Author_US> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(Author_US entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Author_US entity)
        {
            throw new NotImplementedException();
        }
    }
}

using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class Author_USRepository : IAuthor_US
    {
        private readonly string _connectionString;

        public Author_USRepository(IConfiguration configuration)
        {
            // Get connection string from configuration
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> DeleteById(int id)
        {
            // Implement the logic for deleting by ID
            throw new NotImplementedException();
        }

        public async Task<List<Author_US>> GetAll()
        {
            List<Author_US> authors = new List<Author_US>(); // Initialize the list to store the authors

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(); // Open the connection asynchronously

                using (SqlCommand cmd = new SqlCommand("GetAllAuthors_US", conn))
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
            }

            return authors; // Return the list of authors
        }

        public async Task<Author_US> GetById(int id)
        {
            // Implement the logic for getting an author by ID
            throw new NotImplementedException();
        }

        public async Task<int> Insert(Author_US entity)
        {
            int newAuthorID = 0;
            try
            {
               
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();

                        // Create the command
                        using (SqlCommand cmd = new SqlCommand("InsertAuthor_US", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add input parameters
                            cmd.Parameters.AddWithValue("@AuthorName", entity.AuthorName);

                            if (entity.BirthDate.HasValue)
                                cmd.Parameters.AddWithValue("@BirthDate", entity.BirthDate.Value);
                            else
                                cmd.Parameters.AddWithValue("@BirthDate", DBNull.Value);

                            // Add the output parameter
                            SqlParameter outputParam = new SqlParameter("@AuthorID", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            // Execute the command
                            cmd.ExecuteNonQuery();

                            // Retrieve the output parameter value
                            newAuthorID = Convert.ToInt32(outputParam.Value);
                        }
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return newAuthorID;
        }

        public async Task<int> Update(Author_US entity)
        {
            // Implement the logic for updating an author
            throw new NotImplementedException();
        }
    }
}

using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class MediaWithCategoryRepository : IMediaWithCategory
    {
        public readonly IDbConnection _db;
        public MediaWithCategoryRepository(IConfiguration configuration) {

            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public int delete(int id)
        {
           int ID= _db.Execute("[dbo].[DeleteMediaById]",new{ @MediaID=id});
            return ID;

        }

        public List<MediaWithCategory> GetMediaWithCategories()
        {
            List<MediaWithCategory> mediaWithCategories = _db.Query<MediaWithCategory>("GetMediaWithCategorySP").ToList();
            return mediaWithCategories;
        }

        public MediaWithCategory GetMediaWithCategoryById(int id)
        {
            MediaWithCategory mediaWithCategory = _db.QueryFirstOrDefault<MediaWithCategory>("GetMediaDetailsById",new { @MediaID= id });
            return mediaWithCategory;
        }

        public int insert(MediaWithCategory mediaWithCategory)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Title", mediaWithCategory.Title);
            parameters.Add("@MediaType", mediaWithCategory.MediaType);
            parameters.Add("@ReleaseDate", mediaWithCategory.ReleaseDate);
            parameters.Add("@Rating", mediaWithCategory.Rating);
            parameters.Add("@CategoryID", mediaWithCategory.CategoryID);
            parameters.Add("@InsertedMediaID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Execute the stored procedure
            _db.Execute("[dbo].[InsertMedia]", parameters, commandType: CommandType.StoredProcedure);

            // Retrieve the output parameter
            int insertedMediaID = parameters.Get<int>("@InsertedMediaID");

            return insertedMediaID;

        }

        public int update(MediaWithCategory mediaWithCategory)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@MediaID", mediaWithCategory.MediaID, DbType.Int32);
            parameters.Add("@Title", mediaWithCategory.Title, DbType.String);
            parameters.Add("@MediaType", mediaWithCategory.MediaType, DbType.String);
            parameters.Add("@ReleaseDate", mediaWithCategory.ReleaseDate, DbType.Date);
            parameters.Add("@Rating", mediaWithCategory.Rating);
            parameters.Add("@CategoryID", mediaWithCategory.CategoryID, DbType.Int32);

            var updatedMediaID = _db.Execute("[dbo].[UpdateMedia]",parameters);

            return updatedMediaID;
        }
    }
}

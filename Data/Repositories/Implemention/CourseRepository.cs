using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories.Interfaces;
using DomainModels;
using Dapper;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implemention
{
    public class CourseRepository : ICourse
    {
        public readonly IDbConnection _connection;

        public CourseRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

       public List<Course> Courses()
       {
            List<Course> list = _connection.Query<Course>("GetAllCourses").ToList();
            return list;
       }

        public bool delete(int id)
        {
           int ID= _connection.Execute("DeleteCourse",new { @CourseID=id });
            return ID != 0 ? true :false;
        }

        public Course GetById(int id)
        {
            Course course = _connection.QueryFirstOrDefault<Course>("GetCourseById",new { @CourseID =id});
            return course;
        }

        public int insert(Course course)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CourseName", course.CourseName, DbType.String);
            parameters.Add("@CourseDescription", course.CourseDescription, DbType.String);
            parameters.Add("@Credits", course.Credits, DbType.Int32);
            parameters.Add("@DepartmentID", course.DepartmentID, DbType.Int32);
            parameters.Add("@CreatedDate", course.CreatedDate, DbType.DateTime);
            parameters.Add("@ModifiedDate", course.ModifiedDate, DbType.DateTime);
            parameters.Add("@NewCourseID", dbType:DbType.Int32,direction:ParameterDirection.Output);
            _connection.Execute("InsertCourse", parameters);
            return parameters.Get<int>("@NewCourseID");
        }

        public int update(Course course)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CourseID", course.CourseID, DbType.String);
            parameters.Add("@CourseName", course.CourseName, DbType.String);
            parameters.Add("@CourseDescription", course.CourseDescription, DbType.String);
            parameters.Add("@Credits", course.Credits, DbType.Int32);
            parameters.Add("@DepartmentID", course.DepartmentID, DbType.Int32);
            parameters.Add("@ModifiedDate", course.ModifiedDate, DbType.DateTime);
            parameters.Add("@UpdatedCourseID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute("UpdateCourse", parameters);
            return parameters.Get<int>("@UpdatedCourseID");
        }
    }
}

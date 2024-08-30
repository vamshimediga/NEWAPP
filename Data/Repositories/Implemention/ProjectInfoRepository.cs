using Dapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implemention
{
    public class ProjectInfoRepository : IProjectInfo
    {
        public readonly IDbConnection _connection;
        public ProjectInfoRepository(IConfiguration configuration) {

            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public int deleteProjectInfo(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProjectID", id);
            _connection.Execute("DeleteProjectsInfo", parameters);
            return parameters.Get<int>("@ProjectID");
        }

        public ProjectInfo GetProjectInfoById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProjectId", id);
            ProjectInfo projectInfo = _connection.QueryFirst<ProjectInfo>("GetProjectById", parameters);
            return projectInfo;

        }

        public List<ProjectInfo> GetProjects()
        {
           List<ProjectInfo> projectInfos = _connection.Query<ProjectInfo>("GetProjectsInfo").ToList();
            return projectInfos;
        }

        public int insertProjectInfo(ProjectInfo projectInfo)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProjectName", projectInfo.ProjectName, DbType.String, ParameterDirection.Input, 255);
            parameters.Add("@StartDate", projectInfo.StartDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("@EndDate", projectInfo.EndDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("@Budget", projectInfo.Budget, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@Status", projectInfo.Status, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@NewProjectID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute("InsertProjectsInfo", parameters);
            return parameters.Get<int>("@NewProjectID");
        }

        public int updateProjectInfo(ProjectInfo projectInfo)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@ProjectId", projectInfo.ProjectId);
            parameters.Add("@ProjectName", projectInfo.ProjectName);
            parameters.Add("@StartDate", projectInfo.StartDate);
            parameters.Add("@EndDate", projectInfo.EndDate);
            parameters.Add("@Budget", projectInfo.Budget);
            parameters.Add("@Status", projectInfo.Status);
            parameters.Add("@UpdatedProjectId",  DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute("UpdateProject", parameters);
            return parameters.Get<int>("@UpdatedProjectId");
        }
    }
}

using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IProjectInfo
    {
        List<ProjectInfo> GetProjects();

        ProjectInfo GetProjectInfoById(int id);

        int insertProjectInfo(ProjectInfo projectInfo);

        int updateProjectInfo(ProjectInfo projectInfo);

        int deleteProjectInfo(int id);      
    }
}

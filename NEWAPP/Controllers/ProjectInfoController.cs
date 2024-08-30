using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DomainModels;

namespace NEWAPP.Controllers
{
    public class ProjectInfoController : Controller
    {
        public readonly IProjectInfo _projectInfo;
        public ProjectInfoController(IProjectInfo projectInfo) { 
        _projectInfo = projectInfo;
        
        }
        // GET: ProjectInfoController
        public ActionResult Index()
        {
            List<ProjectInfo> ProjectInfo =_projectInfo.GetProjects();
            return View(ProjectInfo);
        }

        // GET: ProjectInfoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjectInfoController/Create
        public ActionResult Create()
        {
           ProjectInfo projectInfo = new ProjectInfo();
            return View(projectInfo);
        }

        // POST: ProjectInfoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectInfo personInfo)
        {
            try
            {
                if (personInfo == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    int id = _projectInfo.insertProjectInfo(personInfo);
                 
                    return Json(new { id = id });
                }
                return View(personInfo);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectInfoController/Edit/5
        public ActionResult Edit(int id)
        {
            ProjectInfo projectInfo = _projectInfo.GetProjectInfoById(id);
            return View(projectInfo);
        }

        // POST: ProjectInfoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,ProjectInfo projectInfo)
        {
            try
            {
                if (projectInfo == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    int Id =_projectInfo.updateProjectInfo(projectInfo);
                    return Json(new { Id = Id });
                }
                return View(projectInfo);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectInfoController/Delete/5
        public ActionResult Delete(int id)
        {
            ProjectInfo projectInfo = _projectInfo.GetProjectInfoById(id);
            return View(projectInfo);
        }

        // POST: ProjectInfoController/Delete/5
        [HttpPost]
       
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int Id = _projectInfo.deleteProjectInfo(id);
                return Json(new { success = true });
            }
            catch
            {
                return View();
            }
        }
    }
}

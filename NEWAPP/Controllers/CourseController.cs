using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace NEWAPP.Controllers
{
    public class CourseController : Controller
    {
        // GET: CourseController
        private readonly ICourse _courses;
        public CourseController(ICourse courses)
        {
            _courses = courses;
        }
       
        public ActionResult Index()
        {
            List<Course> courses = _courses.Courses();
            return View(courses);
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            Course course = new Course();
            return View(course);
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            try
            {
                int id=_courses.insert(course);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            Course course = _courses.GetById(id);
            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            try
            {
                int id = _courses.update(course);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // POST: CourseController/Delete/5
        [HttpPost]
        public ActionResult DeleteCourse(int id)
        {
            try
            {
                // Assuming you have a repository or context to handle database operations
                bool isDeleted = _courses.delete(id);

                if (isDeleted)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Course not found or could not be deleted." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}

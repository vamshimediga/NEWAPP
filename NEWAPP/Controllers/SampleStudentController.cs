using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class SampleStudentController : Controller
    {
        public readonly ISampleStudent _sampleStudent;
        public SampleStudentController(ISampleStudent sampleStudent)
        {

            _sampleStudent = sampleStudent;

        }
        // GET: SampleStudentController
        public ActionResult Index()
        {
            List<SampleStudent> sampleStudents= _sampleStudent.GetStudents();
            return View(sampleStudents);
        }

        // GET: SampleStudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SampleStudentController/Create
        public ActionResult Create()
        {
            return View();
        }




        // POST: SampleStudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SampleStudentController/Edit/5
        public ActionResult Edit(int id)
        {
            SampleStudent sampleStudent = _sampleStudent.GetStudent(id);
            return View(sampleStudent);
        }

        // POST: SampleStudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SampleStudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SampleStudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

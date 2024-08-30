using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: EmployeeController

        private readonly IEmployees _employeesRepository;

        public EmployeeController(IEmployees employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }
        public ActionResult Index()
        {
            List<Employees> employees = _employeesRepository.GetEmployees();
            return View(employees);
        }

        public ActionResult Edit(int id)
        {
            Employees employees = _employeesRepository.GetEmployeesByid(id);
            return View(employees);

        }

        // POST: EmployeeController/Edit/5
        [HttpPost]

        public ActionResult Edit( Employees employees)
        {
            try
            {
                int ID = _employeesRepository.UpdateEmployees(employees);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Details/5


        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            Employees employees = new Employees();
            return View(employees);
        }

        // POST: EmployeeController/Create
        [HttpPost]
       
        public ActionResult Create(Employees employees)
        {
            try
            {
                int Id = _employeesRepository.InsertEmployees(employees);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
       

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            Employees employees= _employeesRepository.GetEmployeesByid(id);
            return View(employees);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        
        public ActionResult Delete(int id, IFormCollection collection)
        {
            //try
            //{
            //    bool b = _employeesRepository.DeleteEmployees(id);
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}

            try
            {
                bool success = _employeesRepository.DeleteEmployees(id);
                return Json(new { success = success });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
    }
}

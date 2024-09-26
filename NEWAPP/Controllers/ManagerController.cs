using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class ManagerController : Controller
    {
        // GET: ManagerController
        private readonly IMapper _mapper;
        public readonly IManager _manager;
        public ManagerController(IManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            List<Manager> managers = _manager.GetManagers();
            List<ManagerViewModel> viewModel = _mapper.Map<List<ManagerViewModel>>(managers);
            return View(viewModel);
            
        }

        // GET: ManagerController/Details/5
        public ActionResult Details(int id)
        {

            Manager manager = _manager.GetManager(id);
            ManagerViewModel viewModel = _mapper.Map<ManagerViewModel>(manager);
            return View(viewModel);
        }

        // GET: ManagerController/Create
        public ActionResult Create()
        {
            Manager manager = new Manager();
            // Populate the email list
            var emailList = new List<SelectListItem>()
            {
            new SelectListItem { Text = "mediga-vamshi@priyanet.com", Value = "mediga-vamshi@priyanet.com" },
            new SelectListItem { Text = "srikanth-kanipuri@priyanet.com", Value = "srikanth-kanipuri@priyanet.com" }
            };

            ViewBag.EmailList = emailList;

            return View(manager);
        }

        // POST: ManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manager manager)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    int id = _manager.Insert(manager);
                    return RedirectToAction("Index");
                }
                return View(manager);



            }
            catch
            {
                return View();
            }
        }

        // GET: ManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            Manager manager= _manager.GetManager(id);
            return View(manager);
        }

        // POST: ManagerController/Edit/5
        [HttpPost]
        
        public ActionResult Edit(Manager manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = _manager.Update(manager);
                    return RedirectToAction(nameof(Index));
                }
                return View(manager);
            }
            catch
            {
                return View();
            }
        }

        // GET: ManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManagerController/Delete/5
        [HttpPost]
        
        public ActionResult Delete([FromBody] List<int> selectedIds)
        {
            try
            {
                bool success=_manager.Delete(selectedIds);
                return Json(new {success=success});
            }
            catch
            {
                return View();
            }
        }
    }
}

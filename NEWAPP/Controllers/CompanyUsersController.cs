using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Filters.AuthorizationFilter;

namespace NEWAPP.Controllers
{
    [ServiceFilter(typeof(CustomAuthorizationFilte))]
    public class CompanyUsersController : Controller
    {

        private readonly ICompanyUsers _companyUsersRepository;
        public CompanyUsersController(ICompanyUsers companyUsersRepository)
        {
            _companyUsersRepository = companyUsersRepository;
        }
        // GET: CompanyUsersController
        public async Task<ActionResult> Index()
        {
            List<CompanyUsers> companyUsers =await _companyUsersRepository.GetCompanyUsers();
            return View(companyUsers);
        }

        // GET: CompanyUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyUsersController/Create
        public ActionResult Create()
        {
            CompanyUsers companyUsers = new CompanyUsers();
            return PartialView("_Create",companyUsers);
        }

        // POST: CompanyUsersController/Create
        [HttpPost]
        
        public async Task<ActionResult> Create(CompanyUsers companyUsers)
        {
            try
            {
                int id =await _companyUsersRepository.insertCompanyUsers(companyUsers);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyUsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CompanyUsers companyUsers = await _companyUsersRepository.GetCompanyUsersbyid(id);
            return PartialView("_EditCompanyUser", companyUsers);
        }

        // POST: CompanyUsersController/Edit/5
        [HttpPost]
        
        public async Task<ActionResult> Edit(CompanyUsers companyUsers)
        {
            try
            {
                int id = await _companyUsersRepository.updateCompanyUsers(companyUsers);  
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyUsersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            CompanyUsers companyUsers = await _companyUsersRepository.GetCompanyUsersbyid(id);
            return  PartialView("_Delete",companyUsers);
        }

        // POST: CompanyUsersController/Delete/5
        [HttpPost]
       
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                bool success = await _companyUsersRepository.deleteCompanyUsers(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> GetAll()
        {

            List<CompanyUsers> companyUsers = await _companyUsersRepository.GetCompanyUsers();
            return Json( new { data=companyUsers });

        }
    }
}

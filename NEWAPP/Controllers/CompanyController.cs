using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class CompanyController : Controller
    {
        public readonly ICompany _company;
        public readonly IMapper _mapper;
        public CompanyController(ICompany company,IMapper mapper)
        {
            _company = company;
            _mapper = mapper;
        }
        // GET: CompanyController
        public async Task<ActionResult> Index()
        {
            List<Company> companies = await _company.GetAll();
            List<CompanyViewModel> viewModel = _mapper.Map<List<CompanyViewModel>>(companies);
            return View(viewModel);
        }

        // GET: CompanyController/Details/5
      

        // GET: CompanyController/Create
        public ActionResult Create()
        {
            CompanyViewModel viewModel = new CompanyViewModel();
            return View(viewModel);
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyViewModel company)
        {
            try
            {
               Company company1=_mapper.Map<Company>(company);  
                int id = await _company.insert(company1);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Company company = await _company.GetById(id);
            CompanyViewModel viewModel =_mapper.Map<CompanyViewModel>(company);
            return View(viewModel);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Edit(CompanyViewModel company)
        {
            try
            {
                Company company1 =_mapper.Map<Company>(company);
                int id =await _company.update(company1);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyController/Delete/5
     

        // POST: CompanyController/Delete/5
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

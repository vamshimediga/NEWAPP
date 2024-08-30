using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace NEWAPP.Controllers
{
    public class PsplCompaniesController : Controller
    {
        // GET: PsplCompaniesController
        public readonly IPsplCompany _psplCompany; 

        public PsplCompaniesController(IPsplCompany psplCompany)
        {
            _psplCompany = psplCompany;
        }
        public ActionResult Index()
        {
            List<PsplCompany> list = _psplCompany.GetPsplCompanies();
            return View(list);
        }

        // GET: PsplCompaniesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PsplCompaniesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PsplCompaniesController/Create
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

        // GET: PsplCompaniesController/Edit/5
        public ActionResult Edit(int id)
        {
            PsplCompany psplCompany = _psplCompany.GetPsplCompany(id);
            return View(psplCompany);
        }

        // POST: PsplCompaniesController/Edit/5
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

        // GET: PsplCompaniesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PsplCompaniesController/Delete/5
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

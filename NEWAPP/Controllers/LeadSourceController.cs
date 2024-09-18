using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class LeadSourceController : Controller
    {
        public readonly ILeadSource _leadSource;
        public LeadSourceController(ILeadSource leadSource)
        {
            _leadSource = leadSource;
        }
        // GET: api/<LeadSourceController>
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            List<LeadSource> leadSources = await _leadSource.GetleadSources();
            return View(leadSources); // Pass the data to the view
        }


        // GET: LeadSourceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeadSourceController/Create
        public ActionResult Create()
        {
            LeadSource LeadSource = new LeadSource();
            return View(LeadSource);
        }

        // POST: LeadSourceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeadSource leadSource)
        {
            try
            {
                int id= await _leadSource.Insert(leadSource);    
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeadSourceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeadSourceController/Edit/5
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

        // GET: LeadSourceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeadSourceController/Delete/5
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

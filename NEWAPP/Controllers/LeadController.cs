using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class LeadController : Controller
    {
        // GET: LeadController
        public ILeads _leadrepo;
        public LeadController(ILeads leads) {
        
        _leadrepo = leads;
        }
        public ActionResult Index()
        
        {
            List<Lead> leads = _leadrepo.Leads();
            return View(leads);
        }

        // GET: LeadController/Details/5
        public ActionResult Details(int id)
        {
            Lead lead = new Lead();
            return View(lead);
        }

        // GET: LeadController/Create
        public ActionResult Create()
        {
            Lead lead = new Lead();
            return View(lead);
        }

        // POST: LeadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lead model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = _leadrepo.insert(model);
                    return Json(new { success = true, message = "Lead created successfully" });
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it) and return an error response
                    return Json(new { success = false, message = ex.Message });
                }
            }

            // If the model state is invalid, return the validation errors
            return Json(new { success = false, message = "Model validation failed", errors = ModelState.Values });
        }

        // GET: LeadController/Edit/5
        public ActionResult<IEnumerable<Lead>> Edit(int id)
        {
            Lead lead = _leadrepo.LeadById(id);
            return View(lead);
        }

        // POST: LeadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lead lead)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int ID = _leadrepo.update(lead);
                    return RedirectToAction(nameof(Index));
                }
                   return View(lead);
            }
            catch
            {
                return View();
            }
        }

        // GET: LeadController/Delete/5
        [HttpPost]
        public JsonResult DeleteSelectedLeads([FromBody] string[] selectedLeadIds)
        {
            if (selectedLeadIds != null && selectedLeadIds.Length > 0)
            {
                // Call the repository method to delete the leads
                int deletedCount = _leadrepo.delete(selectedLeadIds);

                // Return a success response
                return Json(true);
            }

            // Return an error response if no IDs were provided
            return Json(false);
        }





    }
}

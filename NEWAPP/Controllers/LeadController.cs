using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using Newtonsoft.Json;

namespace NEWAPP.Controllers
{
    public class LeadController : Controller
    {
        // GET: LeadController
        public ILeads _leadrepo;
        private readonly IMapper _mapper; // Inject AutoMapper
        public LeadController(ILeads leads,IMapper mapper) {
        
            _leadrepo = leads;
            _mapper = mapper;
        }
        public ActionResult Index()
        
        {
            List<Lead> leads = _leadrepo.Leads();
            List<LeadViewModel> viewModel = _mapper.Map<List<LeadViewModel>>(leads);
            return View(viewModel);
        }

        // GET: LeadController/Details/5
        public ActionResult Details(int id)
        {
            LeadViewModel lead = new LeadViewModel();
            return View(lead);
        }

        // GET: LeadController/Create
        public ActionResult Create()
        {
            LeadViewModel lead = new LeadViewModel();
            return View(lead);
        }

        // POST: LeadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeadViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string json = JsonConvert.SerializeObject(model);
                    // Deserialize JSON string back to LeadSourceViewModel (just for demo)
                    LeadViewModel LeadViewModel = JsonConvert.DeserializeObject<LeadViewModel>(json);
                    // Map ViewModel back to Domain Model
                    Lead lead = _mapper.Map<Lead>(LeadViewModel);
                    int id = _leadrepo.insert(lead);
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

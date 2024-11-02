using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class AccessTableController : Controller
    {
        private readonly ILogger<AccessTableController> _logger;
        public readonly IAccessTable _accessTable;
        public readonly IMapper _mapper;
        public readonly ExceptionLogger _exceptionLogger;
        // GET: AccessTableController
        public AccessTableController(IAccessTable accessTable,IMapper mapper, ILogger<AccessTableController> logger,ExceptionLogger exceptionLogger) { 
        
          _accessTable = accessTable;
          _mapper = mapper;  
          _logger = logger;
          _exceptionLogger = exceptionLogger;

        }
        public async Task<ActionResult> Index()
        {
            List<AccessTable> viewModel =  await _accessTable.GetAllAccessTablesAsync();
            string jsonResult = JsonConvert.SerializeObject(viewModel);
            List<AccessTable> accessTables = JsonConvert.DeserializeObject<List<AccessTable>>(jsonResult);
            List<AccessTableViewModel> Model = _mapper.Map<List<AccessTableViewModel>>(accessTables);
            return View(Model);
        }

        public async Task<ActionResult> Search(string searchPermissionLevel)
        {
             List<AccessTableViewModel> Model = new List<AccessTableViewModel>();
            if (!String.IsNullOrEmpty(searchPermissionLevel))
            {
                List<AccessTable> viewModel = await _accessTable.GetAllAccessTablesAsync();
                List<AccessTable> selectedViewModel = viewModel.Where(a => a.PermissionLevel == searchPermissionLevel).ToList(); // Ensure to use ToList() to convert the result to a List
                Model = _mapper.Map<List<AccessTableViewModel>>(selectedViewModel);
            }
            ViewData["searchPermissionLevel"] = searchPermissionLevel;
            return View("index",Model);
        }

        // GET: AccessTableController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccessTableController/Create
        public ActionResult Create()
        {
            AccessTableViewModel viewModel = new AccessTableViewModel();
            return View(viewModel);
        }

        // POST: AccessTableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccessTableViewModel accessTableViewModel)
        {
            try
            {
                // Map ViewModel to the entity
                AccessTable accessTable = _mapper.Map<AccessTable>(accessTableViewModel);

                // Call the insert method
                int id = await _accessTable.insert(accessTable);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)  // Log the exception
            {
                // Log the error details
                //_logger.LogError(ex, "An error occurred while creating an AccessTable record.");
                _exceptionLogger.LogException(ex, "An error occurred while creating an AccessTable record.");

                // Return the view with the error
                return View("Create", accessTableViewModel);
            }
        }

        // GET: AccessTableController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            AccessTable  accessTable = await _accessTable.GetAccessTableByIdAsync(id);
            AccessTableViewModel accessTableViewModel = _mapper.Map<AccessTableViewModel>(accessTable);
            return View(accessTableViewModel);
        }

        // POST: AccessTableController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AccessTableViewModel accessTableViewModel)
        {
            try
            {
                AccessTable accessTable =_mapper.Map<AccessTable>(accessTableViewModel);    
                int id =await _accessTable.update(accessTable);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //// GET: AccessTableController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: AccessTableController/Delete/5
        [HttpPost]
      
        public async Task<ActionResult> Delete(int accessID)
        {
            try
            {
                int ID= await _accessTable.delete(accessID);
                if (ID != 0)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Error deleting record." });
            }
            catch
            {
                return View();
            }
        }
    }
}

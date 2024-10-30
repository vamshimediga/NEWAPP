using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class AccessTableController : Controller
    {
        public readonly IAccessTable _accessTable;
        public readonly IMapper _mapper;
        // GET: AccessTableController
        public AccessTableController(IAccessTable accessTable,IMapper mapper) { 
        
         _accessTable = accessTable;
         _mapper = mapper;  

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
            return View();
        }

        // POST: AccessTableController/Create
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

        // GET: AccessTableController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccessTableController/Delete/5
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

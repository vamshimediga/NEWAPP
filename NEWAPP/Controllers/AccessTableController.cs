using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

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
        public async Task<ActionResult> Index(object accessTables)
        {
            List<AccessTable> viewModel =  await _accessTable.GetAllAccessTablesAsync();
            List<AccessTableViewModel> Model = _mapper.Map<List<AccessTableViewModel>>(viewModel); 
            return View(Model);
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
        public ActionResult Edit(int id)
        {
            return View();
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

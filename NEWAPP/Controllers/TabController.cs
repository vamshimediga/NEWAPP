using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class TabController : Controller
    {
        // GET: TabController
        public readonly ITab _tabRepo;
        public TabController(ITab tabRepo)
        {
            _tabRepo = tabRepo;
        }
        public ActionResult Index()
        {
            List<Tab> tabs = _tabRepo.GetTabs();
            return View(tabs);
        }

      

        // GET: TabController/Create
        public ActionResult Create()
        {
            Tab tab = new Tab();
            return View(tab);
        }

        // POST: TabController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tab tab)
        {
            try
            {
                int id = _tabRepo.insertTabs(tab);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TabController/Edit/5
        public ActionResult Edit(int id)
        {
            Tab tab = _tabRepo.GetTabById(id);
            return View(tab);
        }

        // POST: TabController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tab tab)
        {
            try
            {
                int Id =_tabRepo.updateTabs(tab);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TabController/Delete/5
        public ActionResult Delete(int id)
        {
            Tab tab =_tabRepo.GetTabById(id);
            return View(tab);
        }

        // POST: TabController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                bool success = _tabRepo.deleteTabs(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

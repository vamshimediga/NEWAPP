using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class HRMGERController : Controller
    {
        public readonly IHRMGER _hRMGER;

        public HRMGERController(IHRMGER hRMGER) {
        
        _hRMGER = hRMGER;
        
        }
        // GET: HRMGERController
        public ActionResult Index()
        {
            List<HRMGER> hRMGERs = _hRMGER.GetHRMGER();
            return View(hRMGERs);
        }

        // GET: HRMGERController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HRMGERController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRMGERController/Create
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

        // GET: HRMGERController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HRMGERController/Edit/5
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

        // GET: HRMGERController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HRMGERController/Delete/5
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

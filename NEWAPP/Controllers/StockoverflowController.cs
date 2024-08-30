using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Filters.AuthorizationFilter;
using NEWAPP.Filters.ExceptionFilter;

namespace NEWAPP.Controllers
{
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class StockoverflowController : Controller
    {
        // GET: StockoverflowController
        public readonly IStockoverflow _stockoverflowRepo;
        public StockoverflowController(IStockoverflow stockoverflow) { 
        _stockoverflowRepo = stockoverflow;
        }
        [HttpGet]
        [Route("/")]
        [Route("StockoverflowList")]
        public ActionResult Index()
        {
            List<Stockoverflow> stockoverflows = _stockoverflowRepo.GetStockoverflows();
             return View(stockoverflows);
        }

        // GET: StockoverflowController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StockoverflowController/Create
        public ActionResult Create()
        {
            Stockoverflow stockoverflow = new Stockoverflow();
            return View(stockoverflow);
        }

     

        [HttpPost]
        public ActionResult Create([FromBody] Stockoverflow stockoverflow)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = _stockoverflowRepo.insertStockoverflow(stockoverflow);
                    return Json(new { success = true, id = id });
                }
                else
                {
                    return Json(new { success = false, message = "Validation failed. Please check your input." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while creating the record." });
            }
        }


        // GET: StockoverflowController/Edit/5
        public ActionResult Edit(int id)
        {
            Stockoverflow stockoverflow = _stockoverflowRepo.GetStockoverflowById(id);
            return View(stockoverflow);
        }

        // POST: StockoverflowController/Edit/5
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

        // GET: StockoverflowController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StockoverflowController/Delete/5
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

        [HttpPost]
        public JsonResult IsEmailAvailable(string email)
        {
            Stockoverflow stockoverflow = _stockoverflowRepo.IsEmailExistsAsync(email);
            return Json(stockoverflow);
        }
    }
}

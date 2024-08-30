using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class DefectiveProductsController : Controller
    {
        // GET: DefectiveProductsController
        
        public readonly IDefectiveProducts _defectiveProducts;
        public DefectiveProductsController(IDefectiveProducts defectiveProducts)
        {
            _defectiveProducts = defectiveProducts;
        }

        [HttpGet]
        [Route("DefectiveProductsALL")]
        public ActionResult Index()
        
        
        {
            HttpContext.Session.SetString("UserName", "JohnDoe");
            HttpContext.Session.SetInt32("UserAge", 30);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true
            };

            Response.Cookies.Append("MyCookie", "cookieValue", options);
            ViewBag.cookieValue = Request.Cookies["MyCookie"];
            List<DefectiveProducts> defectiveProducts = _defectiveProducts.GetProducts();

            return View(defectiveProducts);
        }

        // GET: DefectiveProductsController/Details/5
        [HttpGet]
        [Route("DefectiveProducts/Details/{id}")]
        public ActionResult Details(int id)
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userAge = HttpContext.Session.GetInt32("UserAge");
            ViewBag.UserName = userName;
            ViewBag.UserAge = userAge;
            ViewBag.cookieValue = Request.Cookies["MyCookie"];
            DefectiveProducts defectiveProducts = _defectiveProducts.GetProductsById(id);
            return View(defectiveProducts);
        }

        // GET: DefectiveProductsController/Create
        public ActionResult Create()
        {
            DefectiveProducts defectiveProducts = new ();
            return View(defectiveProducts);
        }

        // POST: DefectiveProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DefectiveProducts defectiveProducts)
        {
            try
            {
                int id = _defectiveProducts.insert(defectiveProducts);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DefectiveProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            DefectiveProducts defectiveProducts =_defectiveProducts.GetProductsById(id);    
            return View(defectiveProducts);
        }

        // POST: DefectiveProductsController/Edit/5
        [HttpPost]
        
        public ActionResult Edit(DefectiveProducts defectiveProducts)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    int id = _defectiveProducts.update(defectiveProducts);
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Invalid data." });
                
            }
            catch
            {
                return View();
            }
        }

        // GET: DefectiveProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            DefectiveProducts defectiveProducts = _defectiveProducts.GetProductsById(id);
            return PartialView("_Delete",defectiveProducts);
        }

        // POST: DefectiveProductsController/Delete/5
        [HttpPost]
       
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                bool success =_defectiveProducts.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}

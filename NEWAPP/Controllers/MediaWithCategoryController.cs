using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Filters.AuthorizationFilter;

namespace NEWAPP.Controllers
{
   // Apply custom filter
    public class MediaWithCategoryController : Controller
    {
        public readonly IMediaWithCategory _media;
        public MediaWithCategoryController(IMediaWithCategory media) {
        
        _media = media; 
        }
        // GET: MediaWithCategoryController
       
        public ActionResult Index()
        {
            List<MediaWithCategory> list = _media.GetMediaWithCategories();
            return View(list);
        }

        // GET: MediaWithCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MediaWithCategoryController/Create
        public ActionResult Create()
        {
            MediaWithCategory mediaWithCategory = new MediaWithCategory();
            return View(mediaWithCategory);
        }

        // POST: MediaWithCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MediaWithCategory mediaWithCategory)
        {
            try
            {
                

                if (ModelState.IsValid)
                {
                    // Perform the creation logic here
                    int id = _media.insert(mediaWithCategory);
                    return Json(new { success = true });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, errors });
                }
            }
            catch
            {
                // Handle exceptions
                return Json(new { success = false, message = "An error occurred" });
            }
        }


        // GET: MediaWithCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            MediaWithCategory mediaWithCategory =_media.GetMediaWithCategoryById(id);
            return View(mediaWithCategory);
        }

        // POST: MediaWithCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MediaWithCategory mediaWithCategory)
        {
            try
            {
                int id = _media.update(mediaWithCategory);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Assuming _media.delete(id) performs the deletion and returns the number of affected rows
            int affectedRows = _media.delete(id);

            if (affectedRows ==-1)
            {
                // Return a success response
                return Json(new { success = true });
            }
            else
            {
                // Return an error response
                return Json(new { success = false, message = "Error deleting item." });
            }
        }


        // POST: MediaWithCategoryController/Delete/5
        
    }
}

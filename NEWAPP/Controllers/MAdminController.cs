using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class MAdminController : Controller
    {
        // GET: MAdminController
        public readonly IMAdmin _mAdmin;
        public readonly IUserAdmin _UserAdmin;
        public MAdminController(IMAdmin mAdmin, IUserAdmin userAdmin)
        {
            _mAdmin = mAdmin;
            _UserAdmin = userAdmin;
        }
        public ActionResult Index()
        {
            List<MAdmin> list = _mAdmin.GetMAdmins();
            return View(list);
        }

        // GET: MAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MAdminController/Create
        [HttpPost]
      
        public ActionResult Create(MAdmin mAdmin)
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

        // GET: MAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            MAdmin mAdmin = _mAdmin.GetMAdminByid(id);
            if (mAdmin == null)
            {
                return Json(new { success = false });
            }

            mAdmin.Flag = !mAdmin.Flag;
            if (mAdmin.Flag)
            {
                int Id = _mAdmin.Insert(new MAdmin()
                {
                    Id = mAdmin.Id,
                    Username = mAdmin.Username,
                    Password = mAdmin.Password,
                    Email = mAdmin.Email,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Flag = mAdmin.Flag,
                });
            }
           

            return Json(new { success = true, flag = mAdmin.Flag });

        }

        // POST: MAdminController/Edit/5
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

        // GET: MAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MAdminController/Delete/5
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

        [HttpGet]
        public IActionResult FileUpload()
        {
            return View();
        }

        // POST: FileUpload
        [HttpPost]
        public async Task<IActionResult> FileUpload(FileUploadModel model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                // Get the file name and combine it with the target directory path
                var fileName = Path.GetFileName(model.File.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads"));

                // Save the file to the target directory
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                ViewBag.Message = "File uploaded successfully!";
                ViewBag.FileName = fileName;
            }
            else
            {
                ViewBag.Message = "Please select a file.";
            }

            return View();
        }

    }
}

using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class UserAdminController : Controller
    {
        // GET: UserAdminController
        public IUserAdmin _userAdmin;
        public UserAdminController(IUserAdmin userAdmin) {
        _userAdmin = userAdmin;
        }  
        public ActionResult Index()
        {
            List<UserAdmin> userAdmins = _userAdmin.GetUserAdmins();
            return View(userAdmins);
        }

        // GET: UserAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserAdminController/Create
        public ActionResult Create()
        {
            UserAdmin userAdmin = new UserAdmin();
            return View(userAdmin);
        }

        // POST: UserAdminController/Create
        [HttpPost]
     
        public ActionResult Create(UserAdmin userAdmin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id =_userAdmin.InsertUserAdmin(userAdmin);
                    return Json(new { id });
                }
                return View(userAdmin);
            }
            catch
            {
                return View();
            }
        }

        // GET: UserAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserAdminController/Edit/5
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

        // GET: UserAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserAdminController/Delete/5
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

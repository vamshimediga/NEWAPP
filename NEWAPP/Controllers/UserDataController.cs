using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class UserDataController : Controller
    {
        public readonly IUserData _userData;
        public UserDataController(IUserData userData)
        {
            _userData = userData;
        }
        public async Task<ActionResult> Index()
        {
            List<UserData> UserData = await _userData.UserDataAsync();
            return View(UserData);
        }

        // GET: UserDataController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserDataController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDataController/Create
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

        // GET: UserDataController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserDataController/Edit/5
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

        // GET: UserDataController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserDataController/Delete/5
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

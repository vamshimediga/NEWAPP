using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NEWAPP.Controllers
{
    public class UserradiochkdropController : Controller
    {
        public readonly IUserradiochkdrop _userradiochkdrop;

        public UserradiochkdropController(IUserradiochkdrop userradiochkdrop)
        {

            _userradiochkdrop = userradiochkdrop;
        }
        // GET: UserradiochkdropController
        public ActionResult Index()
        {
            List<Userchkradio> userchkradios = _userradiochkdrop.GetUserchkradio();
            return View(userchkradios);
        }

        // GET: UserradiochkdropController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserradiochkdropController/Create
        public ActionResult Create()
        {
            ViewBag.CountryList = GetCountries();
            ViewBag.GenderOptions = GetGenderOptions();
            Userchkradio userchkradio = new Userchkradio();
            return View(userchkradio);
        }

        // POST: UserradiochkdropController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Userchkradio userchkradio)
        {
            try
            {
                int id = _userradiochkdrop.InsertUserchkradio(userchkradio);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserradiochkdropController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserradiochkdropController/Edit/5
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

        // GET: UserradiochkdropController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserradiochkdropController/Delete/5
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

        private IEnumerable<SelectListItem> GetCountries()
        {
            // Replace with your actual data fetching logic
            return new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "USA" },
            new SelectListItem { Value = "2", Text = "Canada" },
            new SelectListItem { Value = "3", Text = "UK" }
        };
        }

        private IEnumerable<SelectListItem> GetGenderOptions()
        {
            return new List<SelectListItem>
        {
            new SelectListItem { Value = "M", Text = "Male" },
            new SelectListItem { Value = "F", Text = "Female" }
        };
        }
    }
}

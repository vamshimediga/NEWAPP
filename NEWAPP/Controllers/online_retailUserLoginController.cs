using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class online_retailUserLoginController : Controller
    {
        // GET: online_retailUserLoginController
        public readonly Ionline_retailUserLogin _ionline_RetailUserLogin;
        public readonly IMapper _mapper;
        public online_retailUserLoginController(Ionline_retailUserLogin ionline_RetailUserLogin,IMapper mapper) { 
        _ionline_RetailUserLogin = ionline_RetailUserLogin;
        _mapper=mapper;
        }
        public async Task<ActionResult> Index()
        {
            List<online_retailUserLogin> online_RetailUserLogins = await _ionline_RetailUserLogin.GetOnline_RetailUserLogins();
            List<online_retailUserLoginClientViewModel> online_RetailUserLoginClientViewModels = _mapper.Map<List<online_retailUserLoginClientViewModel>>(online_RetailUserLogins);
            return View("index", online_RetailUserLoginClientViewModels);
        }

        // GET: online_retailUserLoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: online_retailUserLoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: online_retailUserLoginController/Create
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

        // GET: online_retailUserLoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: online_retailUserLoginController/Edit/5
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

        // GET: online_retailUserLoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: online_retailUserLoginController/Delete/5
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

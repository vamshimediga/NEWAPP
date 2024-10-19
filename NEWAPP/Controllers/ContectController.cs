using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class ContectController : Controller
    {
        public readonly IContect _contact;
        public readonly IMapper _mapper;
        public ContectController(IContect contact,IMapper mapper)
        {
            _contact = contact;
            _mapper= mapper;
        }
        // GET: Controller
        public async Task<ActionResult> Index()
        {
            List<Contect> contects = await _contact.GetContects();
            List<ContectViewModel> viewModels = _mapper.Map<List<ContectViewModel>>(contects);
            return View(viewModels);
        }

        // GET: Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Controller/Create
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

        // GET: Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Controller/Edit/5
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

        // GET: Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Controller/Delete/5
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

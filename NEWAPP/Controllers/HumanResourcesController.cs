using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class HumanResourcesController : Controller
    {
        // GET: HumanResourcesController
        public readonly IHumanResources _humanResourcesRepo;
        private readonly IMapper _mapper;
        public HumanResourcesController(IHumanResources humanResources, IMapper mapper)
        {
            _humanResourcesRepo = humanResources;
            _mapper = mapper;
        }
        public async Task<ActionResult> Index()
        {
            List<HumanResources> humanResources = await _humanResourcesRepo.GetHumanResources();
            List<HumanResourcesViewModel> humanResourcesDTOs = _mapper.Map<List<HumanResourcesViewModel>>(humanResources);
            return View(humanResourcesDTOs);
        }

        // GET: HumanResourcesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HumanResourcesController/Create
        public ActionResult Create()
        {
            HumanResources humanResources = new HumanResources();
            return View(humanResources);
        }

        // POST: HumanResourcesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HumanResources humanResources)
        {
            try
            {
                int id = await _humanResourcesRepo.Insert(humanResources);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HumanResourcesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HumanResources humanResources = await _humanResourcesRepo.GetHumanResourcesById(id);
            return View(humanResources);
        }

        // POST: HumanResourcesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HumanResources humanResources)
        {
            try
            {
                int id = await _humanResourcesRepo.Update(humanResources);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HumanResourcesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HumanResourcesController/Delete/5
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

using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using DomainModels;
namespace NEWAPP.Controllers
{
    public class HumanResources : Controller
    {
        // GET: HumanResourcesController
        public readonly IHumanResources _humanResourcesRepo;
        private readonly IMapper _mapper;
        public HumanResources(IHumanResources humanResources, IMapper mapper)
        {
            _humanResourcesRepo = humanResources;
            _mapper = mapper;
        }
        public async Task<ActionResult> Index()
        {
            List<DomainModels.HumanResources> humanResources = await _humanResourcesRepo.GetHumanResources();
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
            DomainModels.HumanResources humanResources = new DomainModels.HumanResources();
            return View(humanResources);
        }

        // POST: HumanResourcesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DomainModels.HumanResources humanResources)
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
            DomainModels.HumanResources humanResources = await _humanResourcesRepo.GetHumanResourcesById(id);
            return View(humanResources);
        }

        // POST: HumanResourcesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DomainModels.HumanResources humanResources)
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
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(string Ids)
        {

            bool flag = await _humanResourcesRepo.Delete(Ids);
            return RedirectToAction(nameof(Index));
        }
    }
}

using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class BodyguardController : Controller
    {
        // GET: BodyguardController
        public readonly IBodyguard _bodyguard;
        public readonly IMapper _mapper;    
        public BodyguardController(IBodyguard bodyguard,IMapper mapper)
        {
            _bodyguard = bodyguard;
            _mapper = mapper;   

        }
        public async Task<ActionResult> Index()
        {
            List<Bodyguard> bodyguards = await _bodyguard.Get();
            List<BodyguardViewModel> bodyguardViewModel = _mapper.Map<List<BodyguardViewModel>>(bodyguards);
            return View(bodyguardViewModel);
        }

        // GET: BodyguardController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Bodyguard bodyguard = await _bodyguard.GetByid(id);
            BodyguardViewModel bodyguardViewModel = _mapper.Map<BodyguardViewModel>(bodyguard);
            return View(bodyguardViewModel);
        }

        // GET: BodyguardController/Create
        public ActionResult Create()
        {
            BodyguardViewModel bodyguardViewModel = new BodyguardViewModel();
            return View(bodyguardViewModel);
        }

        // POST: BodyguardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BodyguardViewModel bodyguardViewModel)
        {
            try
            {
                Bodyguard bodyguard = _mapper.Map<Bodyguard>(bodyguardViewModel);
                int id =await _bodyguard.insert(bodyguard);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BodyguardController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Bodyguard bodyguard = await _bodyguard.GetByid(id);
            BodyguardViewModel bodyguardViewModel = _mapper.Map<BodyguardViewModel>(bodyguard);
            return View(bodyguardViewModel);
        }

        // POST: BodyguardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BodyguardViewModel bodyguardViewModel)
        {
            try
            {
                Bodyguard bodyguard = _mapper.Map<Bodyguard>(bodyguardViewModel);
                int id = await _bodyguard.update(bodyguard);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BodyguardController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Bodyguard bodyguard = await _bodyguard.GetByid(id);
            BodyguardViewModel bodyguardViewModel = _mapper.Map<BodyguardViewModel>(bodyguard);
            return View(bodyguardViewModel);
          //  return View();
        }

        // POST: BodyguardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                int ID = await _bodyguard.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NEWAPP.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NEWAPP.Controllers
{
    public class IITInstituteController : Controller
    {
        // GET: IITInstituteController


        public readonly IITInstitute _iTInstitute;
        public readonly IMapper _mapper;
        public IITInstituteController(IITInstitute iTInstitute,IMapper mapper)
        {
            _iTInstitute = iTInstitute;
            _mapper = mapper;   
        }
        public async Task<ActionResult> Index()
        
        {
            List<ITInstitute>  iTInstitutes = await _iTInstitute.GetITInstitutes();
            List<ITInstituteViewModel> iTInstituteViewModels = _mapper.Map<List<ITInstituteViewModel>>(iTInstitutes);
            return View(iTInstituteViewModels);
        }

        // GET: IITInstituteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IITInstituteController/Create
        public ActionResult Create()
        {
            ViewBag.CoursesList = new SelectList(new[]
              {
                  new { Id = 1, Name = "C" },
                  new { Id = 2, Name = "C++" },
                  new { Id = 3, Name = ".NET" },
                  new { Id = 4, Name = "Java" },
                  new { Id = 5, Name = "Ruby" },
                  new { Id = 6, Name = "Angular" },
                  new { Id = 7, Name = "React" }
             }, "Id", "Name");
            ITInstituteViewModel iTInstituteViewModel = new ITInstituteViewModel();
            return View(iTInstituteViewModel);
        }

        // POST: IITInstituteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ITInstituteViewModel iTInstituteViewModel)
        {
            try
            {
                var instituteViewModel = JsonConvert.SerializeObject(iTInstituteViewModel);
                ITInstituteViewModel instituteViewModel1 = JsonConvert.DeserializeObject<ITInstituteViewModel>(instituteViewModel);
                ITInstitute institute = _mapper.Map<ITInstitute>(instituteViewModel1);
                int id = await _iTInstitute.insert(institute);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IITInstituteController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ITInstitute institute = await _iTInstitute.GetById(id); // Fetch institute by ID
            if (institute == null)
            {
               return NotFound();
            }
            // Format the ContactNumber to be in the format 111-111-2323
            institute.ContactNumber = Regex.Replace(institute.ContactNumber, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
            ITInstituteViewModel iTInstituteViewModel = _mapper.Map<ITInstituteViewModel>(institute);
            return View("Edit",iTInstituteViewModel);
        }

        // POST: IITInstituteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ITInstituteViewModel iTInstituteViewModel)
        {
            try
            {
                var instituteViewModel = JsonConvert.SerializeObject(iTInstituteViewModel);
                ITInstituteViewModel instituteViewModel1 = JsonConvert.DeserializeObject<ITInstituteViewModel>(instituteViewModel);
                ITInstitute institute = _mapper.Map<ITInstitute>(instituteViewModel1);
                int id = await _iTInstitute.update(institute);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IITInstituteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IITInstituteController/Delete/5
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

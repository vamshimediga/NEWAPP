using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
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
            return View();
        }

        // POST: IITInstituteController/Create
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

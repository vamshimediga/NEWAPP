using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class FlatController : Controller
    {
        private readonly IFlat _flat;
        private readonly IMapper _mapper;
        private readonly IPropertyOwners _propertyOwners;
        public FlatController(IFlat flat,IMapper mapper,IPropertyOwners propertyOwners) { 
            _flat = flat;
            _mapper = mapper;
            _propertyOwners = propertyOwners;
        
        }
        // GET: FlatController
        public async Task<ActionResult> Index()
        {
            List<Flat> flats = await _flat.GetFlatsAsync();
            List<FlatViewModel> flatViewModel = _mapper.Map<List<FlatViewModel>>(flats);
            return View(flatViewModel);
        }

        // GET: FlatController/Details/5
        public  ActionResult Details(int id)
        {
           // await _flat.Delete(id);
            return View();
        }

        // GET: FlatController/Create
        // GET: FlatController/Create
        public async Task<ActionResult> Create()
        {
            FlatViewModel viewModel = new FlatViewModel();

            // Fetch PropertyOwners list
            List<PropertyOwners> propertyOwners = await _propertyOwners.GetPropertyOwners();

            // Map to the correct ViewModel type
            List<PropertyOwnerViewModel> propertyOwnerViewModels = _mapper.Map<List<PropertyOwnerViewModel>>(propertyOwners);

            // Assign correctly
            viewModel.PropertyOwners = propertyOwnerViewModels;

            return View(viewModel);
        }


        // POST: FlatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FlatViewModel viewModel)
        {
            try
            {
                Flat flat = _mapper.Map<Flat>(viewModel);
                await _flat.Insert(flat);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlatController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Flat flat = await _flat.GetFlatByIdAsync(id);
            FlatViewModel flatViewModel = _mapper.Map<FlatViewModel>(flat);
            // Fetch PropertyOwners list
            List<PropertyOwners> propertyOwners = await _propertyOwners.GetPropertyOwners();

            // Map to the correct ViewModel type
            List<PropertyOwnerViewModel> propertyOwnerViewModels = _mapper.Map<List<PropertyOwnerViewModel>>(propertyOwners);

            // Assign correctly
            flatViewModel.PropertyOwners = propertyOwnerViewModels;
            return View(flatViewModel);
          //  return View();
        }

        // POST: FlatController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, FlatViewModel viewModel)
        {
            try
            {
                Flat flat = _mapper.Map<Flat>(viewModel);
                await _flat.Update(flat);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlatController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Flat flat =  await _flat.GetFlatByIdAsync(id);
            FlatViewModel flatViewModel = _mapper.Map<FlatViewModel>(flat); 
            return View(flatViewModel);
        }

        // POST: FlatController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _flat.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

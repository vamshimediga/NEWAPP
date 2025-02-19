using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class BuildingOwnerController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBuildingOwner _buildingOwner;
        private readonly IConstructionBuilder _constructionBuilder;
        public BuildingOwnerController(IMapper mapper,IBuildingOwner buildingOwner,IConstructionBuilder constructionBuilder) {
        
        _mapper = mapper;
        _buildingOwner = buildingOwner;
         _constructionBuilder = constructionBuilder;
        }
        // GET: BuildingOwnerController
        public async Task<ActionResult> Index()
        {
            List<BuildingOwner> buildingOwners = await _buildingOwner.GetAll();
            List<BuildingOwnerViewModel> buildingOwn = _mapper.Map<List<BuildingOwnerViewModel>>(buildingOwners);
            List<ConstructionBuilder> constructionBuilders = await _constructionBuilder.Get();
            List<ConstructionBuilderViewModel> constructionBuilderViewModels = _mapper.Map<List<ConstructionBuilderViewModel>>(constructionBuilders);

            return View(buildingOwn);
        }


        // GET: BuildingOwnerController/Create
        public async Task<ActionResult> Create()
        {
            BuildingOwnerViewModel buildingOwnerViewModel = new BuildingOwnerViewModel();
            List<ConstructionBuilder> constructionBuilders = await _constructionBuilder.Get();
            List<ConstructionBuilderViewModel> constructionBuilderViewModels = _mapper.Map<List<ConstructionBuilderViewModel>>(constructionBuilders);
            buildingOwnerViewModel.ConstructionBuilders = constructionBuilderViewModels;
            return View(buildingOwnerViewModel);
        }

        // POST: BuildingOwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BuildingOwnerViewModel buildingOwnerViewModel)
        {
            try
            {
                BuildingOwner buildingOwner = _mapper.Map<BuildingOwner>(buildingOwnerViewModel);
                await _buildingOwner.Insert(buildingOwner);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BuildingOwnerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            BuildingOwner buildingOwner =  await _buildingOwner.GetById(id);   
            BuildingOwnerViewModel buildingOwnerViewModel =  _mapper.Map<BuildingOwnerViewModel>(buildingOwner);
            List<ConstructionBuilder> constructionBuilders = await _constructionBuilder.Get();
            List<ConstructionBuilderViewModel> constructionBuilderViewModels = _mapper.Map<List<ConstructionBuilderViewModel>>(constructionBuilders);
            buildingOwnerViewModel.ConstructionBuilders= constructionBuilderViewModels;
            return View(buildingOwnerViewModel);
        }

        // POST: BuildingOwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BuildingOwnerViewModel buildingOwnerViewModel)
        {
            try
            {
                BuildingOwner buildingOwner =_mapper.Map<BuildingOwner>(buildingOwnerViewModel);
                await _buildingOwner.Update(buildingOwner);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BuildingOwnerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            BuildingOwner buildingOwner = await _buildingOwner.GetById(id);
            BuildingOwnerViewModel buildingOwnerViewModel = _mapper.Map<BuildingOwnerViewModel>(buildingOwner);
            List<ConstructionBuilder> constructionBuilders = await _constructionBuilder.Get();
            List<ConstructionBuilderViewModel> constructionBuilderViewModels = _mapper.Map<List<ConstructionBuilderViewModel>>(constructionBuilders);
            buildingOwnerViewModel.ConstructionBuilders = constructionBuilderViewModels;
            return View(buildingOwnerViewModel);
         
        }

        // POST: BuildingOwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _buildingOwner.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

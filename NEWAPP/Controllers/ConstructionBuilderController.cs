using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{

    public class ConstructionBuilderController : Controller
    {
        private readonly IConstructionBuilder _constructionBuilder;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBuildingOwner _buildingOwner;
        public ConstructionBuilderController(IConstructionBuilder constructionBuilder,IMapper mapper,IBuildingOwner buildingOwner) {
        _constructionBuilder = constructionBuilder;
        _mapper = mapper;
        _buildingOwner = buildingOwner;
        }
        // GET: ConstructionBuilderController
        public async Task<ActionResult> Index()
        {
            List<ConstructionBuilder> constructionBuilders = await _constructionBuilder.Get();
            List<ConstructionBuilderViewModel> viewModels = _mapper.Map<List<ConstructionBuilderViewModel>>(constructionBuilders);
            return View(viewModels);
        }

        

        // GET: ConstructionBuilderController/Create
        public ActionResult Create()
        {
            ConstructionBuilderViewModel constructionBuilderViewModel = new ConstructionBuilderViewModel();

            return View(constructionBuilderViewModel);
        }

        // POST: ConstructionBuilderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ConstructionBuilderViewModel constructionBuilderViewModel)
        {
            try
            {
                ConstructionBuilder constructionBuilder =  _mapper.Map<ConstructionBuilder>(constructionBuilderViewModel);
                await _constructionBuilder.Insert(constructionBuilder);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConstructionBuilderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
           ConstructionBuilder constructionBuilder = await _constructionBuilder.GetConstructorById(id);
           ConstructionBuilderViewModel viewModel = _mapper.Map<ConstructionBuilderViewModel>(constructionBuilder);
            return View(viewModel);
        }

        // POST: ConstructionBuilderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ConstructionBuilderViewModel viewModel)
        {
            try
            {
                ConstructionBuilder constructionBuilder = _mapper.Map<ConstructionBuilder>(viewModel);
               _constructionBuilder.Update(constructionBuilder);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConstructionBuilderController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            ConstructionBuilder constructionBuilder = await _constructionBuilder.GetConstructorById(id);
            ConstructionBuilderViewModel viewModel = _mapper.Map<ConstructionBuilderViewModel>(constructionBuilder);
            return View(viewModel);
        }

        // POST: ConstructionBuilderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _constructionBuilder.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

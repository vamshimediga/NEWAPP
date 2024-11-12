using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class CookingRecipeController : Controller
    {
        // GET: CookingRecipeController
        public readonly ICookingRecipe _cookingRecipe;
        public readonly IMapper _mapper;
        public CookingRecipeController(ICookingRecipe cookingRecipe,IMapper mapper) {
            _cookingRecipe = cookingRecipe;
            _mapper = mapper;   
        }    
        public async Task<ActionResult> Index()
        {
            List<CookingRecipe> CookingRecipe = await _cookingRecipe.GetcookingRecipesAsync();
            List<CookingRecipeViewModel> viewModels = _mapper.Map<List<CookingRecipeViewModel>>(CookingRecipe);
             return View(viewModels);
        }

        // GET: CookingRecipeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CookingRecipeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CookingRecipeController/Create
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

        // GET: CookingRecipeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CookingRecipe cookingRecipe = await _cookingRecipe.GetcookingRecipebyid(id);
            CookingRecipeViewModel cookingRecipeViewModel = _mapper.Map<CookingRecipeViewModel>(cookingRecipe);
            return View(cookingRecipeViewModel);
        }

        // POST: CookingRecipeController/Edit/5
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

        // GET: CookingRecipeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CookingRecipeController/Delete/5
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

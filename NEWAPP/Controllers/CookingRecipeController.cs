using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ActionResult> Search(string name)
        {
            List<CookingRecipe> CookingRecipe = await _cookingRecipe.GetSearchCookingRecipes(name);
            List<CookingRecipeViewModel> viewModels = _mapper.Map<List<CookingRecipeViewModel>>(CookingRecipe);
            return View("index",viewModels);
        }

        // GET: CookingRecipeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            CookingRecipe cookingRecipe = await _cookingRecipe.GetcookingRecipebyid(id);
            CookingRecipeViewModel cookingRecipeViewModel = _mapper.Map<CookingRecipeViewModel>(cookingRecipe);
            return View(cookingRecipeViewModel);
            
        }
        public async Task<ActionResult> Images()
        {
            List<Image> images = await _cookingRecipe.GetImages();
            List<ImageViewModel> viewModels = _mapper.Map<List<ImageViewModel>>(images);
            return View(viewModels);
        }
        public async Task<ActionResult> UploadImage()
        {
            ImageViewModel imageViewModel = new ImageViewModel();
            return View(imageViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(ImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        // Copy the file to memory stream
                        await model.ImageFile.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();
                        var base64String = Convert.ToBase64String(imageBytes);

                        // Create the Image entity and map the data
                        Image image = new Image
                        {
                            ImageName = model.ImageName,
                            ImageData = base64String
                        };
                             // Save to database
                        int id = await _cookingRecipe.insertimagebase64(image);

                        if (id > 0)
                        {
                            return RedirectToAction("Images");
                        }
                        ModelState.AddModelError("", "Image upload failed.");
                    }
                }
            }

            return View(model);
        }


        // GET: CookingRecipeController/Create
        public ActionResult Create()
        {
            CookingRecipeViewModel cookingRecipeView = new CookingRecipeViewModel();
            return View(cookingRecipeView);
        }

        // POST: CookingRecipeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CookingRecipeViewModel cookingRecipeViewModel)
        {
            try
            {
                CookingRecipe cookingRecipe = _mapper.Map<CookingRecipe>(cookingRecipeViewModel);
                int id = await _cookingRecipe.insert(cookingRecipe);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                
                return View("Create", cookingRecipeViewModel);
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

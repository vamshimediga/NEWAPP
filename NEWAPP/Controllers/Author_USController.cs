using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class Author_USController : Controller
    {
        private readonly IAuthor_US _author_US;
        private readonly IMapper _mapper; // Inject AutoMapper
        // GET: Author_USController
        public Author_USController(IAuthor_US author_US,IMapper mapper)
        {
            _author_US = author_US;
            _mapper = mapper;   

        }
        public async Task<ActionResult> Index()
        {
            List<Author_US> list = await _author_US.GetAll();
            List<Author_USViewModel> viewModels = _mapper.Map<List<Author_USViewModel>>(list);
            return View(viewModels);
        }
        public async Task<ActionResult> GetAuthors()
        {
            List<Author_US> list = await _author_US.GetAll();
            List<Author_USViewModel> viewModels = _mapper.Map<List<Author_USViewModel>>(list);
            return Json(new { data = viewModels });
        }
        // GET: Author_USController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Author_USController/Create
        public ActionResult Create()
        {
            Author_USViewModel author = new Author_USViewModel();
            return View(author);
        }

        // POST: Author_USController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(Author_USViewModel author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map ViewModel to Entity using AutoMapper
                    Author_US author_US = _mapper.Map<Author_US>(author);

                    // Insert record into database
                    int id = await _author_US.Insert(author_US);

                    // Return success response
                    return Json(new { success = true, message = "Author created successfully!", id = id });
                }

                // Return validation failure response
                return Json(new { success = false, message = "Invalid data. Please check the input fields." });
            }
            catch (Exception ex)
            {
                // Return error response
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }


        // GET: Author_USController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Author_USController/Edit/5
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

        // GET: Author_USController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Author_USController/Delete/5
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

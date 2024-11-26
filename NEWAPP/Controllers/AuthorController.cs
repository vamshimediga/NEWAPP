using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class AuthorController : Controller
    {
        public readonly IAuthor _author;
        public readonly IMapper _mapper;
        public AuthorController(IAuthor author, IMapper mapper)
        {

            _author = author;
            _mapper = mapper;
        }
        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            List<Author> authorList = await _author.GetAuthors();
            List<AuthorViewModel> authorViewModelList = _mapper.Map<List<AuthorViewModel>>(authorList);
             return View(authorViewModelList);
        }
        [HttpGet]
        public IActionResult FileCreate()
        {
            // Example data for dropdown
            ViewBag.FileTypes = new SelectList(new[] { "PDF", "DOCX", "XLSX", "ZIP", "IMG" });
            FileViewModel fileViewModel = new FileViewModel();
            return View(fileViewModel);
        }
        [HttpPost]
        public IActionResult FileCreate(FileViewModel fileViewModel)
        {
           
            return View();
        }
        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            AuthorViewModel authorViewModel = new AuthorViewModel();    
            return View(authorViewModel);
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AuthorViewModel authorViewModel)
        {
            try
            {
                Author author = _mapper.Map<Author>(authorViewModel);
                await _author.insert(author);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthorController/Edit/5
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

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthorController/Delete/5
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

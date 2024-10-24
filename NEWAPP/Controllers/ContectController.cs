using AutoMapper;
using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class ContectController : Controller
    {
        public readonly IContect _contact;
        public readonly IMapper _mapper;
        public ContectController(IContect contact,IMapper mapper)
        {
            _contact = contact;
            _mapper= mapper;
        }
        // GET: Controller
        public async Task<ActionResult> Index()
        {
            List<Contect> contects = await _contact.GetContects();
            List<ContectViewModel> viewModels = _mapper.Map<List<ContectViewModel>>(contects);
            return View(viewModels);
        }
        public async Task<IActionResult> SearchContect(string firstName, string lastName)
        {
            List<ContectViewModel> viewModels = new List<ContectViewModel>(); // Initialize viewModels

            // If both firstName and lastName are empty, get all contacts
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                ViewData["CurrentFilterFirstName"] = "";
                ViewData["CurrentFilterLastName"] = "";
                List<Contect> contects = await _contact.GetContects();
                viewModels = _mapper.Map<List<ContectViewModel>>(contects);
            }
            else
            {
                if (!string.IsNullOrEmpty(firstName))
                {
                    ViewData["CurrentFilterFirstName"] = firstName;
                }
                else
                {
                    ViewData["CurrentFilterFirstName"] = "";
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    ViewData["CurrentFilterLastName"] = lastName;
                }
                else
                {
                    ViewData["CurrentFilterLastName"] = "";
                }

                List<Contect> contects = (List<Contect>)await _contact.SearchContactsAsync(firstName, lastName);
                viewModels = _mapper.Map<List<ContectViewModel>>(contects);
            }

            // Return the view with the viewModels
            return View("Index", viewModels);
        }


        // GET: Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Controller/Create
        public ActionResult Create()
        {
            ContectViewModel contectView = new ContectViewModel();
            return View("Create", contectView);
        }

        // POST: Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContectViewModel contectView)
        {
            try
            {
                Contect contect = _mapper.Map<Contect>(contectView);
                int id = await _contact.insert(contect);
                if (id != 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                // In case of validation failure or error, return view with a warning message
                ViewBag.ErrorMessage = "Failed to create the contact. Please try again.Enter  Unique email id";
                return View("Create", contectView);
            }
            catch (Exception ex)
            {
                // Pass the error message to the view
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Create", contectView);
            }
        }


        // GET: Controller/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Contect contect = await _contact.GetContectById(id);
            ContectViewModel viewModel = _mapper.Map<ContectViewModel>(contect);    
            return View("Edit",viewModel);
        }

        // POST: Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ContectViewModel contectViewModel)
        {
            try
            {
                Contect contect = _mapper.Map<Contect>(contectViewModel);
                int id= await _contact.update(contect);
                if(id != 0)
                {
                return RedirectToAction(nameof(Index));
                }
                return View("Edit",contectViewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Controller/Delete/5
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

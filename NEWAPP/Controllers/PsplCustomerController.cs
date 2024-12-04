using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class PsplCustomerController : Controller
    {
        public  readonly IPsplCustomer _psplCustomer;
        public readonly IMapper _mapper;
        public PsplCustomerController(IPsplCustomer psplCustomer, IMapper mapper)
        {
            _psplCustomer = psplCustomer;
            _mapper = mapper;
        }

        // GET: PsplCustomerController
        public async Task<ActionResult> Index()
        {
            List<PsplCustomer> psplCustomer = await _psplCustomer.GetPsplCustomers();
            List<PsplCustomerViewModel> viewModel = _mapper.Map<List<PsplCustomerViewModel>>(psplCustomer);
            return View(viewModel);
        }
        // GET: PsplCustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PsplCustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PsplCustomerController/Create
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

        // GET: PsplCustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            PsplCustomer psplCustomer = await _psplCustomer.GetPsplCustomersByidAsync(id);
            PsplCustomerViewModel viewModel = _mapper.Map<PsplCustomerViewModel>(psplCustomer);
            return View(viewModel);
        }

        // POST: PsplCustomerController/Edit/5
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

        // GET: PsplCustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PsplCustomerController/Delete/5
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

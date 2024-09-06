using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class CustomerDATAController : Controller
    {
        // GET: CustomerDATAController
        public readonly ICustomerDATARepository<CustomerData> _customerDATARepository;
        public CustomerDATAController(ICustomerDATARepository<CustomerData> customerDATARepository) {
            _customerDATARepository= customerDATARepository;
        }
        public  async Task<ActionResult> Index()
        {
            List<CustomerData> CustomerData = await _customerDATARepository.GetAllAsync();
            return View(CustomerData);
        }

        // GET: CustomerDATAController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerDATAController/Create
        public ActionResult Create()
        {
            CustomerData customerData = new CustomerData();
            return View(customerData);
        }

        // POST: CustomerDATAController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerData customerData)
        {
            try
            {
                int id = await _customerDATARepository.InsertAsync(customerData);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerDATAController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerDATAController/Edit/5
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

        // GET: CustomerDATAController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerDATAController/Delete/5
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

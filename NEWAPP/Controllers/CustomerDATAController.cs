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
                TempData["Message"] = "Customer data created successfully!";
                TempData["MessageType"] = "success"; // 'success', 'error', 'info', 'warning'
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Message"] = "Error occurred while creating customer data.";
                TempData["MessageType"] = "error";
                return View();
            }
        }

        // GET: CustomerDATAController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CustomerData customerData= await _customerDATARepository.GetByIdAsync(id);
            return View(customerData);
        }

        // POST: CustomerDATAController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerData customerData)
        {
            try
            {
                int id = await _customerDATARepository.UpdateAsync(customerData);
                TempData["Message"] = "Customer data updated successfully!";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Message"] = "Error occurred while updating customer data.";
                TempData["MessageType"] = "error";
                return View(customerData);
            }
        }

        // GET: CustomerDATAController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            CustomerData customerData = await _customerDATARepository.GetByIdAsync(id);
            return View(customerData);
        }

        // POST: CustomerDATAController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                bool result = await _customerDATARepository.DeleteAsync(id);
                if (result)
                {
                    TempData["Message"] = "Customer data deleted successfully!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error occurred while deleting customer data.";
                    TempData["MessageType"] = "error";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                TempData["Message"] = "Error occurred while deleting customer data.";
                TempData["MessageType"] = "error";
                return View("Error");
            }
        }

        [HttpPost]
        
        public async Task<ActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                {
                    return Json(new { success = false, message = "No customers selected for deletion." });
                }

                bool result = await _customerDATARepository.DeleteMultipleAsync(ids);
                if (result)
                {
                    return Json(new { success = true, message = "Customer data deleted successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Error occurred while deleting customer data." });
                }
            }
            catch
            {
                return Json(new { success = false, message = "An error occurred while deleting customer data." });
            }
        }


    }
}

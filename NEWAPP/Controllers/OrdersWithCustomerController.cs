using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class OrdersWithCustomerController : Controller
    {
        // GET: OrdersWithCustomerController
        public readonly IOrdersWithCustomer _orders;
        public OrdersWithCustomerController(IOrdersWithCustomer orders) { 
            _orders = orders;
        
        }   
        public ActionResult Index()
        {
            List<OrdersWithCustomer> orders = _orders.GetAll();
            return View(orders);
        }

        // GET: OrdersWithCustomerController/Details/5
        public ActionResult Details(int id)
        {
            OrdersWithCustomer ordersWithCustomer = _orders.GetById(id);
            return View(ordersWithCustomer);
        }

        // GET: OrdersWithCustomerController/Create
        public ActionResult Create()
        {
            OrdersWithCustomer ordersWithCustomer = new OrdersWithCustomer();
            return View(ordersWithCustomer);
        }

        // POST: OrdersWithCustomerController/Create
        [HttpPost]
        
        public ActionResult Create(OrdersWithCustomer ordersWithCustomer)
        {
            try
            {
                int id = _orders.insert(ordersWithCustomer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersWithCustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            OrdersWithCustomer ordersWithCustomer = _orders.GetById(id);
            return View(ordersWithCustomer);
        }

        // POST: OrdersWithCustomerController/Edit/5
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

        // GET: OrdersWithCustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrdersWithCustomerController/Delete/5
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

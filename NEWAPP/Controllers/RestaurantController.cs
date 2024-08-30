using Data.Repositories.Interfaces;
using DomainModels;

using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: RestaurantController
        public  IRestaurant _restaurant;
        public RestaurantController(IRestaurant restaurant) {
        
        _restaurant = restaurant;
        }   
        public ActionResult Index()
        {
            List<Restaurant> list = _restaurant.GetAll();
            return View(list);
        }

        // GET: RestaurantController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RestaurantController/Create
        public ActionResult Create()
        {
            Restaurant restaurant = new Restaurant();
            return View(restaurant);
        }

        // POST: RestaurantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        {
            try
            {
                // Your model binding and business logic here
                if (ModelState.IsValid)
                {
                    int id =_restaurant.insert(restaurant);
                    // Save data to the database or perform your desired action
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // If model state is not valid, return the view with the model state
                    return View(restaurant);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                ModelState.AddModelError("", "An error occurred while creating the record: " + ex.Message);
                return View(restaurant);
            }
        }


        // GET: RestaurantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RestaurantController/Edit/5
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

        // GET: RestaurantController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RestaurantController/Delete/5
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

using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class ProductChartController : Controller
    {
        public readonly IProductChart _productChart;
        public ProductChartController(IProductChart productChart)
        {
            _productChart = productChart;
        }
        // GET: ProductChartController
        public ActionResult Index()
        {
            List<ProductChart> productCharts = _productChart.GetProductCharts();
            return View(productCharts);
        }
        
        public ActionResult GetProductData(string product)
        {
            List<ProductChart> productCharts =  _productChart.GetProductCharts();
            ProductChart productData = productCharts.FirstOrDefault<ProductChart>(p => p.Product == product);

            if (productData == null)
            {
                return Json(null);
            }

            ProductChart result = new(){
                Product = productData.Product,
                Q1 = productData.Q1,
                Q2 = productData.Q2,
                Q3 = productData.Q3,
                Q4 = productData.Q4
            };

            return Json(result);
        }
        // GET: ProductChartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductChartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductChartController/Create
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

        // GET: ProductChartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductChartController/Edit/5
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

        // GET: ProductChartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductChartController/Delete/5
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

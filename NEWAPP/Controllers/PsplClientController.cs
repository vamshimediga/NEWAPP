using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class PsplClientController : Controller
    {

        public readonly IPsplClient _psplclient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper; // Inject AutoMapper
        private readonly HttpClient _httpClient;
        public PsplClientController(IPsplClient psplclient, IConfiguration configuration, IMapper mapper, HttpClient httpClient)
        {
            _psplclient = psplclient;
            _configuration = configuration;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        // GET: PsplClientController
        public async Task<ActionResult> Index()
        {
              List<PsplClient> psplClients = await _psplclient.GetAll();
              List<PsplClientViewModel> viewModels = _mapper.Map<List<PsplClientViewModel>>(psplClients);
              return View(viewModels);
        }

        // GET: PsplClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PsplClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PsplClientController/Create
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

        // GET: PsplClientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PsplClientController/Edit/5
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

        // GET: PsplClientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PsplClientController/Delete/5
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

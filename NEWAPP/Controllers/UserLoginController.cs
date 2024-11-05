using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class UserLoginController : Controller
    {
        public readonly IUserLogin _userLogin;
        private readonly ILogger<AccessTableController> _logger;
        public readonly IMapper _mapper;
        public readonly ExceptionLogger _exceptionLogger;
        public readonly HttpClient _httpClient;
        public readonly IConfiguration _configuration;
        // GET: AccessTableController
        public UserLoginController(IUserLogin userLogin,
            IMapper mapper,
            ILogger<AccessTableController> logger,
            ExceptionLogger exceptionLogger,
            HttpClient httpClient,
            IConfiguration configuration)
        {

            _userLogin = userLogin;
            _mapper = mapper;
            _logger = logger;
            _exceptionLogger = exceptionLogger;
            _httpClient = httpClient;
            _configuration = configuration;

        }
        // GET: UserLoginController
        public async Task<ActionResult> Index()
        {
            List<UserLogin> userLogins = await _userLogin.UserLogins();
            List<UserLoginViewModel> users = _mapper.Map<List<UserLoginViewModel>>(userLogins);
            return View(users);
        }

        // GET: UserLoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserLoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserLoginController/Create
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

        // GET: UserLoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserLoginController/Edit/5
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

        // GET: UserLoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserLoginController/Delete/5
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

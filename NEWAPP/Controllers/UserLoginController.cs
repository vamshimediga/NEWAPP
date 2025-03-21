﻿using AutoMapper;
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
            UserLoginViewModel userLoginViewModel = new UserLoginViewModel();
            return View(userLoginViewModel);
        }

        // POST: UserLoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserLoginViewModel userLoginViewModel)
        {
            try
            {
                UserLogin userLogin  = _mapper.Map<UserLogin>(userLoginViewModel);
                string idstr = await _userLogin.insert(userLogin);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserLoginController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            UserLogin userLogin = await _userLogin.UserLoginAsync(id);
            UserLoginViewModel userLoginViewModel = _mapper.Map<UserLoginViewModel>(userLogin);
            return View(userLoginViewModel);
        }

        // POST: UserLoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserLoginViewModel userLoginViewModel)
        {
            try
            {
                UserLogin userLogin = _mapper.Map<UserLogin>(userLoginViewModel);
                string id = await _userLogin.update(userLogin);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       

        // POST: UserLoginController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                try
                {
                 
                   string ids= await  _userLogin.delete(id); // Delete the user
                    return Json(new { success = true });
                }
                catch
                {
                    // In case of any exception, return a failure response
                    return Json(new { success = false });
                }
            }
            catch
            {
                return View();
            }
        }
    }
}

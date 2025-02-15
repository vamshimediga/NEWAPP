using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class UsersListController : Controller
    {
        private readonly ILogger<AccessTableController> _logger;
        public readonly IMapper _mapper;
        public readonly ExceptionLogger _exceptionLogger;
        public readonly HttpClient _httpClient;
        public readonly IConfiguration _configuration;
        private readonly IUsersList _userslist;
        private readonly IUserRoles _userroles;
        // GET: AccessTableController
        public UsersListController(IUsersList userslist,
            IUserRoles userroles,
            IMapper mapper,
            ILogger<AccessTableController> logger,
            ExceptionLogger exceptionLogger,
            HttpClient httpClient,
            IConfiguration configuration)
        {

        //    _userLogin = userLogin;
            _mapper = mapper;
            _logger = logger;
            _exceptionLogger = exceptionLogger;
            _httpClient = httpClient;
            _configuration = configuration;
            _userslist = userslist;
            _userroles = userroles;
        }
        // GET: UsersListController
        public async Task<ActionResult> Index()
        {
            // Fetch user list from repository
            List<UsersList> usersLists = await _userslist.GetUsers();
            List<UsersListViewModel> viewModels = _mapper.Map<List<UsersListViewModel>>(usersLists);

            // Fetch user roles
            List<UserRoles> userRoles = await _userroles.GetUserRoles();
            List<UserRolesViewModel> userRolesViewModels = _mapper.Map<List<UserRolesViewModel>>(userRoles);

            // Assign roles to each user in the list
            foreach (var user in viewModels)
            {
                // Filter the roles by RoleID for each user
                user.Roles = userRolesViewModels.Where(r => r.RoleID == user.RoleID).ToList();
            }

            return View(viewModels);
        }



        // GET: UsersListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersListController/Create
        public async Task<ActionResult> Create()
        {
            UsersListViewModel model = new UsersListViewModel();
            List<UserRoles> userRoles = await _userroles.GetUserRoles();
            List<UserRolesViewModel> userRolesViewModels = _mapper.Map<List<UserRolesViewModel>>(userRoles);
            model.Roles = userRolesViewModels;
            return View(model);
        }

        // POST: UsersListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsersListViewModel model)
        {
            try
            {
                UsersList usersList = _mapper.Map<UsersList>(model);

                int id = await _userslist.insert(usersList);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersListController/Edit/5
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

        // GET: UsersListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersListController/Delete/5
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

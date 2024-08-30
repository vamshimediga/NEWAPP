using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NEWAPP.Controllers
{

    public class AdminUsersController : Controller
    {
        // GET: AdminUsersController
        public readonly IAdminUsers _adminUsers;
        public AdminUsersController(IAdminUsers adminUsers) {
        
            _adminUsers = adminUsers;
        
        }
        public async Task<ActionResult> Index()
        
        {
            List<AdminUser> adminUsers = await _adminUsers.GetAdminUsersAsync();
            return View(adminUsers);
        }

        // GET: AdminUsersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            AdminUser adminUser = await _adminUsers.GetAdminUserByIdAsync(id);
            return View(adminUser);
        }

        // GET: AdminUsersController/Create
        public ActionResult Create()
        {
            AdminUser adminUser = new AdminUser();
            return View(adminUser);
        }

        // POST: AdminUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdminUser  adminUser)
        {
            try
            {
                if (adminUser == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    int id =await _adminUsers.InsertAdminUser(adminUser);
                    return Ok(id);
                }
                return View(adminUser);
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminUsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            AdminUser adminUser = await _adminUsers.GetAdminUserByIdAsync(id);
            return View(adminUser);
        }

        // POST: AdminUsersController/Edit/5
        [HttpPost]
      
        public async Task<ActionResult> Edit(int id, AdminUser adminUser)
        {
            try
            {
                if (adminUser == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    int Id = await _adminUsers.UpdateAdminUserAsync(adminUser);
                    return Ok(Id);
                }
                return View(adminUser);
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminUsersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            AdminUser adminUser = await _adminUsers.GetAdminUserByIdAsync(id);
            return View(adminUser);
        }

        // POST: AdminUsers/Delete/5
        [HttpPost]
       
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                int ID= await _adminUsers.DeleteAdminUserAsync(id);
                return Ok(ID);
            }
            catch
            {
                return View();
            }
        }
    }
}

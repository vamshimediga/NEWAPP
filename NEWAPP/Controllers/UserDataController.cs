using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using Newtonsoft.Json;

namespace NEWAPP.Controllers
{
    public class UserDataController : Controller
    {
        public readonly IUserData _userData;
        public readonly IMapper _mapper;
        public UserDataController(IUserData userData,IMapper mapper)
        {
            _userData = userData;
            _mapper = mapper;
        }
        public async Task<ActionResult> Index()
        {
           
            List<UserData> UserData = await _userData.UserDataAsync();
            List<UserDataViewModel> viewModel = _mapper.Map<List<UserDataViewModel>>(UserData);
            return View(viewModel);
        }

      
      
        // GET: UserDataController/Create
        public ActionResult Create()
        {
            UserDataViewModel userDataViewModel = new UserDataViewModel();
            return View(userDataViewModel);
        }

        // POST: UserDataController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserDataViewModel userDataViewModel)
        {
            try
            {
                if (userDataViewModel == null) { 
                   return NotFound();
                }
                else
                {
                    string json = JsonConvert.SerializeObject(userDataViewModel);
                    // Deserialize JSON string back to LeadSourceViewModel (just for demo)
                    UserDataViewModel userDataviewmodel = JsonConvert.DeserializeObject<UserDataViewModel>(json);
                    // Map ViewModel back to Domain Model
                    UserData userData = _mapper.Map<UserData>(userDataviewmodel);
                    await _userData.InsertUserData(userData);
                   return RedirectToAction(nameof(Index));
                }
               
            }
            catch
            {
                return View(userDataViewModel);
            }
        }

        // GET: UserDataController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            UserData userData = await _userData.UserDataById(id);
            UserDataViewModel viewModel = _mapper.Map<UserDataViewModel>(userData);
            return View(viewModel);
        }

        // POST: UserDataController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserDataViewModel userDataViewModel)
        {
            try
            {
                if (userDataViewModel == null)
                {
                    return NotFound();
                }
                else
                {
                    string json = JsonConvert.SerializeObject(userDataViewModel);
                    // Deserialize JSON string back to LeadSourceViewModel (just for demo)
                    UserDataViewModel userDataviewmodel = JsonConvert.DeserializeObject<UserDataViewModel>(json);
                    // Map ViewModel back to Domain Model
                    UserData userData = _mapper.Map<UserData>(userDataviewmodel);
                    await _userData.UpdateUserData(userData);
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch
            {
                return View();
            }
        }

        // GET: UserDataController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserDataController/Delete/5
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

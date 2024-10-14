using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using NuGet.Packaging.Signing;

namespace NEWAPP.Controllers
{
    public class PersonDataController : Controller
    {
        public readonly IPersonData _personData;
        private readonly IMapper _mapper; // Inject AutoMapper
        public PersonDataController(IPersonData personData, IMapper mapper) {
        _personData = personData;
         _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<PersonData> personDatas = await _personData.GetPersonList();
            List<PersonDataViewModel> viewModel = _mapper.Map<List<PersonDataViewModel>>(personDatas);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            PersonData personData = await _personData.GetPersonById(id);
            PersonDataViewModel viewModel = _mapper.Map<PersonDataViewModel>(personData);
            return View(viewModel);
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string[] ids)
        {
            try
            {
                bool issuccess = await _personData.deletePerson(ids);
                if (issuccess)
                {
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                // Log exception
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = false });
        }
        public async Task<IActionResult> Create()
        {
            PersonDataViewModel personData =  new PersonDataViewModel();
            personData.Address = new AddressDataViewModel();
            return View(personData);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PersonDataViewModel personData)
        {
            PersonData newpersonData = _mapper.Map<PersonData>(personData);
            int id = await _personData.insertPerson(newpersonData);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PersonDataViewModel personData)
        {
           
            try
            {
                PersonData newpersonData = _mapper.Map<PersonData>(personData);
                int id = await _personData.updatePerson(newpersonData);
                if (id > 0)
                {
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                // Log exception
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = false });
        }

    }
}

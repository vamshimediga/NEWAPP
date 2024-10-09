using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace NEWAPP.Controllers
{
    public class PersonDataController : Controller
    {
        public readonly IPersonData _personData;
        public PersonDataController(IPersonData personData) {
        _personData = personData;
        }
        public async Task<IActionResult> Index()
        {
            List<PersonData> personDatas = await _personData.GetPersonList();
            return View(personDatas);
        }

        public async Task<IActionResult> Edit(int id)
        {
            PersonData personData = await _personData.GetPersonById(id);
            return  View(personData);
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
    }
}

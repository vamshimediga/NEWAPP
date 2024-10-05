using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Mvc;

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
    }
}

using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

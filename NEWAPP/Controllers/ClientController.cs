using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClient _clientRepository;
        public ClientController(IClient clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: ClientController

        
        public async  Task<ActionResult> Index()
        {
            List<Client> clients = await _clientRepository.GetClients();
            return View(clients);
        }


        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            Client client = new Client();
            return View(client);
        }

        // POST: ClientController/Create
       

        [HttpPost]
        public async Task<ActionResult> Create(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int Id = await _clientRepository.InsertClient(client);
                    return RedirectToAction(nameof(Index));
                }

                return View(client); // Return the view with validation errors
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }


        // GET: ClientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Client client=await  _clientRepository.GetClientById(id);
            return View(client);
        }

       
        [HttpPost]
        public async Task<ActionResult> Edit(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int Id = await _clientRepository.UpdateClientDetails(client);
                    return RedirectToAction(nameof(Index));
                }
               return View(client);
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        
       
        public async Task<ActionResult> Delete(int[] ids)
        {
          

            try
            {
                bool isSuccess = await _clientRepository.DeleteClients(ids);
                if (isSuccess)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch
            {
                return Json(new { success = false });
            }
        }
    }
}

using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_LAYER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUsersAPIController : ControllerBase
    {

        public readonly IAdminUsers _adminUsers;
        public AdminUsersAPIController(IAdminUsers adminUsers)
        {

            _adminUsers = adminUsers;

        }
        // GET: api/<AdminUsersAPIController>
        [HttpGet("AdminUsers")]
        public async Task<List<AdminUser>> Index()

        {
            List<AdminUser> adminUsers = await _adminUsers.GetAdminUsersAsync();
            return adminUsers;
        }

        // GET api/<AdminUsersAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdminUsersAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AdminUsersAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminUsersAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

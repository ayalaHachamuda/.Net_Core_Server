using DAL.Dtos;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace project_.net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly IAdminUser _dbstoreAdminUser;

        public AdminUserController(IAdminUser dbstoreAdminUser)
        {
            _dbstoreAdminUser = dbstoreAdminUser;
        }
        [HttpGet]
        //[Route("/api/GetAdminUser")]
        
        public async Task<ActionResult<AdminUserDto>> get(int id)
        {
            var res = await _dbstoreAdminUser.GetAdminUser(id);
            if (res == null)
            {
                return NoContent(); // או NotFound() אם אתה מעדיף להחזיר 404
            }
            return Ok(res);

        }

        [HttpPost]
        //[Route("/api/CreateAdminUser")]
        public async Task<ActionResult<bool>> add(AdminUserDto adminUser)
        {
            var res = await _dbstoreAdminUser.AddAdminUser(adminUser);
            return Ok(res);

        }

        [HttpPut]
        //[Route("/api/UpdateAdminUser")]
        public async Task<ActionResult<bool>> put(AdminUserDto adminUser)
        {
            var res = await _dbstoreAdminUser.UpdateAdminUser(adminUser);
            return Ok(res);
        }

        [HttpDelete]
        //[Route("/api/DeleteUser")]
        public async Task<ActionResult<bool>> delete(int id)
        {
            var res = await _dbstoreAdminUser.DeleteAdminUser(id);
            return Ok(res);
        }
    }
}

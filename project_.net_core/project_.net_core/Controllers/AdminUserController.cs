using DAL.Data;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminUserController(IAdminUser dbstoreAdminUser, IHttpContextAccessor httpContextAccessor)
        {
            _dbstoreAdminUser = dbstoreAdminUser;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<ActionResult<AdminUserDto>> get(int id)
        {
            var res = await _dbstoreAdminUser.GetAdminUser(id);
            if (res == null)
            {
                return NoContent(); // או NotFound() אם אתה מעדיף להחזיר 404
            }
            return Ok(res);

        }
       [ HttpGet]
    [Route("GetAllAdminUsers")]
        public async Task<ActionResult<List<AdminUserDto>>> GetAllAdminUsers()
        {
            var adminUsers = await _dbstoreAdminUser.GetAllAdminUsers();
            if (adminUsers == null || !adminUsers.Any())
            {
                return NoContent(); // או NotFound() אם אתה מעדיף להחזיר 404
            }
            return Ok(adminUsers);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> add(AdminUserDto adminUser)
        {
            var res = await _dbstoreAdminUser.AddAdminUser(adminUser);
            return Ok(res);

        }

        [HttpPut]
        public async Task<ActionResult<bool>> put(AdminUserDto adminUser)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "HttpContext is null.");
            }

            var adminIdString = context.Items["User"]?.ToString();

            if (string.IsNullOrEmpty(adminIdString) || !int.TryParse(adminIdString, out var adminId))
            {
                return Unauthorized("User not found in token.");
            }

           
            var res = await _dbstoreAdminUser.UpdateAdminUser(adminUser, adminId);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> delete(int id)
        {
            var res = await _dbstoreAdminUser.DeleteAdminUser(id);
            return Ok(res);
        }
    }
}

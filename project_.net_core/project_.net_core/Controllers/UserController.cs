using DAL.Interface;
using DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Models.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BL.Middlewares;
namespace project_.net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : Controller
    {
        private readonly IUser _dbstoreUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IUser dbstoreUser, IHttpContextAccessor httpContextAccessor)
        {
            _dbstoreUser = dbstoreUser;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> get(int id)
        {
            var res =await _dbstoreUser.GetUser(id);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<List<AdminUserDto>>> GetAllUsers()
        {
            var users = await _dbstoreUser.GetAllUsers();
            if (users == null || !users.Any())
            {
                return NoContent(); // או NotFound() אם אתה מעדיף להחזיר 404
            }
            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> add(UserDto user)
        {
            var res = await _dbstoreUser.AddUser(user);
            return Ok(res);

        }

        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<bool>> put(UserDto user)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "HttpContext is null.");
            }

            var userIdString = context.Items["User"]?.ToString();

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return Unauthorized("User not found in token.");
            }
            
            var res = await _dbstoreUser.UpdateUser(user,userId);
            return Ok(res);
        }

       
        [HttpDelete]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<bool>> delete(int id)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "HttpContext is null.");
            }

            var userIdString = context.Items["User"]?.ToString();
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return Unauthorized("User not found in token.");
            }

            Console.WriteLine($"Role: {userRole}, UserId: {userId}");

            if (userRole == "Admin" || id == userId)
            {
                var res = await _dbstoreUser.DeleteUser(id);
                return Ok(res);
            }

            return Forbid();
        }

    }
}

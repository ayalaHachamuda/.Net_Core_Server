using DAL.Interface;
using DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Models.Models;
using Microsoft.AspNetCore.Authorization;

namespace project_.net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : Controller
    {
        private readonly IUser _dbstoreUser;

        public UserController(IUser dbstoreUser)
        {
            _dbstoreUser = dbstoreUser;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> get(int id)
        {
            var res =await _dbstoreUser.GetUser(id);
            return Ok(res);

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
            var res = await _dbstoreUser.UpdateUser(user);
            return Ok(res);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,User")]

        public async Task<ActionResult<bool>> delete(int id)
        {
            var res = await _dbstoreUser.DeleteUser(id);
            return Ok(res);
        }



    }
}

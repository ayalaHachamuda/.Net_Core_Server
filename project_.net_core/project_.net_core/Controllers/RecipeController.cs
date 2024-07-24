using DAL.Data;
using DAL.Dtos;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace project_.net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RecipeController : Controller
    {
        private readonly IRecipe _dbstoreRecipe;
       
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipeController(IRecipe dbstoreRecipe, IHttpContextAccessor httpContextAccessor)
        {
            _dbstoreRecipe = dbstoreRecipe;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<ActionResult<RecipeDto>> get(int id)
        {
            var res = await _dbstoreRecipe.GetRecipe(id);
            if (res == null)
            {
                return NoContent(); // או NotFound() אם אתה מעדיף להחזיר 404
            }
            return Ok(res);

        }
        [HttpPost()]
        [Authorize(Roles = "Admin,User")]
       
        public async Task<IActionResult> add([FromBody] RecipeDto recipeDto)
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

            var res = await _dbstoreRecipe.AddRecipe(recipeDto, userId);
            return Ok(res);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<bool>> put(RecipeDto recipe)
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
            var res = await _dbstoreRecipe.UpdateRecipe(recipe,userId);
            return Ok(res);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<bool>> delete(int id)
        {
            var res = await _dbstoreRecipe.DeleteRecipe(id);
            return Ok(res);
        }
    }
}

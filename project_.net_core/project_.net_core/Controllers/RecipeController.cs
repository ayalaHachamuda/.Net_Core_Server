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

        public RecipeController(IRecipe dbstoreRecipe)
        {
            _dbstoreRecipe = dbstoreRecipe;
        }
        [HttpGet]
        //[Route("/api/GetRecipe")]
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
        //[Route("/api/CreateCompetition")]
        public async Task<ActionResult<bool>> add(RecipeDto recipeDto)
        {
            var res = await _dbstoreRecipe.AddRecipe(recipeDto);
            return Ok(res);

        }

        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        //[Route("/api/UpdateRecipe")]
        public async Task<ActionResult<bool>> put(RecipeDto recipe)
        {
            var res = await _dbstoreRecipe.UpdateRecipe(recipe);
            return Ok(res);
        }

        [HttpDelete]
        //[Route("/api/DeleteRecipe")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<bool>> delete(int id)
        {
            var res = await _dbstoreRecipe.DeleteRecipe(id);
            return Ok(res);
        }
    }
}

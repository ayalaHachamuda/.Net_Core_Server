using DAL.Dtos;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace project_.net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CompetitionController : Controller
    {
        private readonly ICompetition _dbstoreCompetition;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CompetitionController(ICompetition dbstoreCompetition, IHttpContextAccessor httpContextAccessor)
        {
            _dbstoreCompetition = dbstoreCompetition;
            _httpContextAccessor = httpContextAccessor; 
        }
        [HttpGet]
        public async Task<ActionResult<CompetitionDto>> get(int id)
        {
            var res = await _dbstoreCompetition.GetCompetition(id);
            return Ok(res);

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> add([FromBody] CompetitionDto competitionDto)
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

            var res = await _dbstoreCompetition.AddCompetition(competitionDto, adminId);
            return Ok(res);

        }
       
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> put(CompetitionDto competition)
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
            var res = await _dbstoreCompetition.UpdateCompetition(competition, adminId);
            return Ok(res);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> delete(int id)
        {
            var res = await _dbstoreCompetition.DeleteCompetition(id);
            return Ok(res);
        }
    }
}

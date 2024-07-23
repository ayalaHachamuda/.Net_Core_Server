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

        public CompetitionController(ICompetition dbstoreCompetition)
        {
            _dbstoreCompetition = dbstoreCompetition;
        }
        [HttpGet]
        //[Route("/api/GetCompetition")]
        public async Task<ActionResult<CompetitionDto>> get(int id)
        {
            var res = await _dbstoreCompetition.GetCompetition(id);
            return Ok(res);

        }
        [HttpPost]

        //[Route("/api/CreateCompetition")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> add(CompetitionDto competitionDto)
        {
            var res = await _dbstoreCompetition.AddCompetition(competitionDto);
            return Ok(res);

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        //[Route("/api/UpdateCompetition")]
        public async Task<ActionResult<bool>> put(CompetitionDto competition)
        {
            var res = await _dbstoreCompetition.UpdateCompetition(competition);
            return Ok(res);
        }

        [HttpDelete]
        //[Route("/api/DeleteCompetition")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> delete(int id)
        {
            var res = await _dbstoreCompetition.DeleteCompetition(id);
            return Ok(res);
        }
    }
}

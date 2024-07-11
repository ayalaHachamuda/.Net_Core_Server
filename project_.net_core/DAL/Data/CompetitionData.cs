using AutoMapper;
using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Interface;
using DAL.Dtos;
namespace DAL.Data
{
    public class CompetitionData : ICompetition
    {
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;

        public CompetitionData(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public async Task<List<Competition>> GetAllCompetitions()
        //{
        //    return _mapper.Map<List<Competition>>(await _context.Competitions.ToListAsync());
        //}

        public async Task<CompetitionDto> GetCompetition(int id)
        {
            var competitionUser = await _context.Competitions.FindAsync(id);
            return _mapper.Map<CompetitionDto>(competitionUser);
        }
        public async Task<bool> AddCompetition(CompetitionDto competitionDto)
        {
            var competition = _mapper.Map<Competition>(competitionDto);
            _context.Competitions.AddAsync(competition);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCompetition(CompetitionDto competitionDto)
        {
            var competition = _mapper.Map<Competition>(competitionDto);
            _context.Competitions.Update(competition);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCompetition(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);
            if (competition != null)
            {
                _context.Competitions.Remove(competition);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

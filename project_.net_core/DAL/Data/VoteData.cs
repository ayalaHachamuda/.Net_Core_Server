using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Dtos;
using DAL.Interface;
using Models.Models;
namespace DAL.Data
{
    public class VoteData:IVote
    {
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;

        public VoteData(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<VoteDto> GetVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            return _mapper.Map<VoteDto>(vote);
        }

        //public async Task<List<Recipe>> GetAllRecipes()
        //{
        //    return await _context.Recipes.ToListAsync();
        //}

        public async Task<bool> AddVote(VoteDto voteDto)
        {
            var vote = _mapper.Map<Vote>(voteDto);
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote != null)
            {
                _context.Votes.Remove(vote);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

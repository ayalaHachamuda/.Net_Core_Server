using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DataService: IDataService
    {
        private readonly ProjectContext _context;
        
        public DataService(ProjectContext context)
        {
            _context = context;
        }
        public async Task<bool> HasUserVotedAsync(int userId, int recipeId)
        {
            // הוספת קוד לבדיקת האם המשתמש הצביע כבר
          return await _context.Votes.AnyAsync(v => v.Id == userId && v.RecipeId == recipeId);
            
        }

        public async Task AddVoteAsync(int userId, int recipeId)
        {
           // בדיקה אם המשתמש כבר הצביע עבור המתכון
            bool hasVoted = await HasUserVotedAsync(userId, recipeId);
            if (hasVoted)
            {
                throw new InvalidOperationException("User has already voted for this recipe.");
            }

            DateTime voteDate = DateTime.UtcNow;
            Vote vote = new Vote
            {
                RecipeId = recipeId,
                VoteDate = voteDate
                // UserId צריך להוסיף שדה UserId במודל Vote
                //UserId = userId
            };
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveVoteAsync(int userId, int recipeId)
        {
            var vote = await _context.Votes
       .FirstOrDefaultAsync(v => v.Id == userId && v.RecipeId == recipeId);

            if (vote != null)
            {
                _context.Votes.Remove(vote);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("User has not voted for this recipe.");
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserAsync(int userId)
        {
            // הוספת קוד להסרת משתמש
            throw new NotImplementedException();
        }

        public async Task JoinCompetitionAsync(int userId, int competitionId)
        {
            // הוספת קוד להצטרפות לתחרות
            throw new NotImplementedException();
        }

        public async Task LeaveCompetitionAsync(int userId, int competitionId)
        {
            // הוספת קוד לעזיבת תחרות
            throw new NotImplementedException();
        }

        public async Task<Competition> GetCompetitionByIdAsync(int competitionId)
        {
            // הוספת קוד לקבלת תחרות לפי ID
            throw new NotImplementedException();
        }

        public async Task PublishRecipeAsync(int userId, int competitionId, Recipe recipe)
        {
            // הוספת קוד לפרסום מתכון
            throw new NotImplementedException();
        }
        //---------------------------------------------------------------------------------
        public async Task AddCompetitionAsync(Competition competition)
        {
            // הוספת קוד להוספת תחרות
            _context.Competitions.Add(competition);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }
        //---------------------------------------------------------------------------------
        public async Task RemoveCompetitionAsync(int competitionId)
        {
            // הוספת קוד להסרת תחרות
            throw new NotImplementedException();
        }

        public async Task UpdateCompetitionAsync(Competition competition)
        {
            // הוספת קוד לעדכון תחרות
            throw new NotImplementedException();
        }
    }
}

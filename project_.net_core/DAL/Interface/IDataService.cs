using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IDataService
    {
         Task<bool> HasUserVotedAsync(int userId, int recipeId);
         Task AddVoteAsync(int userId, int recipeId);
         Task RemoveVoteAsync(int userId, int recipeId);
         Task UpdateUserAsync(User user);
         Task RemoveUserAsync(int userId);
        Task JoinCompetitionAsync(int userId, int competitionId);
        Task LeaveCompetitionAsync(int userId, int competitionId);
        Task<Competition> GetCompetitionByIdAsync(int competitionId);
        Task PublishRecipeAsync(int userId, int competitionId, Recipe recipe);
        //ADMINUSER
        Task AddCompetitionAsync(Competition competition);
        Task RemoveCompetitionAsync(int competitionId);
        Task UpdateCompetitionAsync(Competition competition);
    }
}

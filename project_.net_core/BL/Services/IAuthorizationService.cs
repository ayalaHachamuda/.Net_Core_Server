using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;


namespace BL.Services
{
    public interface IAuthorizationService
    {
        Task<bool> CanVoteAsync(User user, int competitionId);
        Task<bool> CanDeleteOrUpdateUserAsync(User user, int userId);
        Task<bool> CanJoinOrLeaveCompetitionAsync(User user, int competitionId);
        Task<bool> CanPublishCompetitionAsync(User user);
        Task<bool> CanPublishRecipeAsync(User user, int competitionId);
        Task<bool> IsAdminAsync(User user);
    }
}

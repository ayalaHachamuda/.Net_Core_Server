using DAL.Interface;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IDataService _dataService;

        public AuthorizationService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<bool> CanVoteAsync(User user, int competitionId)
        {
            if (user is User)
            {

                var hasVoted = await _dataService.HasUserVotedAsync(user.Id, competitionId);
                return !hasVoted;
            }
            return false;
        }

        public async Task<bool> CanDeleteOrUpdateUserAsync(User user, int userId)
        {
            return user is User && user.Id == userId;
        }
        public async Task<bool> CanPublishCompetitionAsync(User user)
        {
            return user is AdminUser;
        }
        public async Task<bool> CanJoinOrLeaveCompetitionAsync(User user, int competitionId)
        {
            return user is User;
        }

        public async Task<bool> CanPublishRecipeAsync(User user, int competitionId)
        {
            if (user is User)
            {

                var competition = await _dataService.GetCompetitionByIdAsync(competitionId);
                var currentDate = DateTime.UtcNow;
                return currentDate >= competition.StartDate && currentDate <= competition.EndDate;
            }
            return false;
        }

        public async Task<bool> IsAdminAsync(User user)
        {
            return user is AdminUser;
        }
    }
}


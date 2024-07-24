
using DAL.Dtos;
using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICompetition
    {
        Task<CompetitionDto> GetCompetition(int id);
        //Task<List<Competition>> GetAllCompetitions();
        Task<bool> AddCompetition(CompetitionDto competition,int adminId);
        Task<bool> UpdateCompetition(CompetitionDto competition, int adminId);
        Task<bool> DeleteCompetition(int id);
    }
}

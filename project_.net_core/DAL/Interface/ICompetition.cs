
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
        Task<bool> AddCompetition(CompetitionDto competition);
        Task<bool> UpdateCompetition(CompetitionDto competition);
        Task<bool> DeleteCompetition(int id);
    }
}

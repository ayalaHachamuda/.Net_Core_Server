using DAL.Dtos;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IVote
    {
        Task<VoteDto> getVote(int id);
        //Task<List<VoteDto>> getAllVotes();
        Task<bool> addVote(VoteDto vote);
        Task<bool> deleteVote(int id);
    }
}

using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos
{
    public class VoteDto
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public DateTime VoteDate { get; set; }
    }
}

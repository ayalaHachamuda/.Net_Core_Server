using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}

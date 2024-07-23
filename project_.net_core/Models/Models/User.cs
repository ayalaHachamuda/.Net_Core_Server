using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Models.Models
//{
//    public class User
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public string Email { get; set; }
//        public string Password { get; set; }

//        public ICollection<Vote> Votes { get; set; }
//    }
//}
namespace Models.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; } // הוסף את השדה תפקיד

        public ICollection<Vote> Votes { get; set; }
    }
}

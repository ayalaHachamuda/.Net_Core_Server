using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //[JsonIgnore]
        public string Role { get; set; } // הוסף את השדה תפקיד
        [JsonIgnore]
        public int Id { get; set; }
        
    }

}

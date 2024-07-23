using BL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

//public struct Login
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public string Email { get; set; }
//    public string Password { get; set; }
//    public Login(string name, string email, string password)
//    {
//        Name = name;
//        Email = email;
//        Password = password;
//    }

//}
public class Login
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

//[HttpPost("login")]
//public IActionResult Login([FromBody] LoginDto login)
//{
//    // כאן את יכולה להוסיף את הלוגיקה שלך לבדוק אם המשתמש מאומת
//    if (login.Username == "test" && login.Password == "password") // דוגמה לבדיקת משתמש
//    {
//        var token = GenerateToken("userId", "Admin"); // החליפי ב-userId נכון
//        return Ok(new { Token = token });
//    }

//    return Unauthorized("Invalid credentials");
//}

namespace project_.net_core.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class LoginController : ControllerBase
    //{
    //    private readonly IConfiguration _config;
    //    private readonly IUserService _user;

    //    public LoginController(IConfiguration config, IUserService user)
    //    {
    //        _config = config;
    //        _user = user;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Post([FromBody] Login loginRequest)
    //    {
    //        var userFind = await _user.GetUser(loginRequest.Id);

    //        if (userFind != null)
    //        {
    //            var tokenHandler = new JwtSecurityTokenHandler();
    //            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
    //            //var tokenDescriptor = new SecurityTokenDescriptor
    //            //{
    //            //    Subject = new ClaimsIdentity(new Claim[]
    //            //    {
    //            //        new Claim(ClaimTypes.Name, userFind.Name),
    //            //        new Claim(ClaimTypes.Email, userFind.Email)
    //            //    }),
    //            //    Expires = DateTime.UtcNow.AddHours(1),
    //            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //            //};
    //            //88888888
    //            //            var tokenDescriptor = new SecurityTokenDescriptor
    //            //            {
    //            //                Subject = new ClaimsIdentity(new[]
    //            //{
    //            //    new Claim(ClaimTypes.Name, userFind.Name),
    //            //    new Claim(ClaimTypes.Email, userFind.Email),
    //            //   new Claim(ClaimTypes.Role, userFind.Role) // Include role claim
    //            //}),
    //            //                Expires = DateTime.UtcNow.AddHours(1),
    //            //                Issuer = _config["Jwt:Issuer"], // אותו ערך כמו בשרת
    //            //                Audience = _config["Jwt:Issuer"], // אותו ערך כמו בשרת
    //            //                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //            //            };
    //            //888888888888
    //            var tokenDescriptor = new SecurityTokenDescriptor
    //            {
    //                Subject = new ClaimsIdentity(new[]
    //            {
    //    new Claim(ClaimTypes.Name, userFind.Name),
    //    new Claim(ClaimTypes.Email, userFind.Email),
    //    new Claim(ClaimTypes.Role, userFind.Role) // Include role claim
    //}),
    //                Expires = DateTime.UtcNow.AddHours(1),
    //                Issuer = _config["Jwt:Issuer"],
    //                Audience = _config["Jwt:Issuer"],
    //                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //            };

    //            var token = tokenHandler.CreateToken(tokenDescriptor);
    //            var tokenString = tokenHandler.WriteToken(token);

    //            return Ok(new { Token = tokenString });
    //        }
    //        else
    //        {
    //            return Unauthorized();
    //        }
    //    }
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _user;

        public LoginController(IConfiguration config, IUserService user)
        {
            _config = config;
            _user = user;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login loginRequest)
        {
            var userFind = await _user.GetUser(loginRequest.Id);

            if (userFind != null && userFind.Password == loginRequest.Password)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, userFind.Name),
                    new Claim(ClaimTypes.Email, userFind.Email),
                    new Claim(ClaimTypes.Role, userFind.Role)
                }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Issuer"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }

}


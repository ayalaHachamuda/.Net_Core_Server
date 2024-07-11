using AutoMapper;
using DAL.Dtos;
using DAL.Interface;
using Models.Models;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class UserData:IUser
    {  
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;

        public UserData
            
            (ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //public async Task<List<Competition>> GetAllCompetitions()
        //{
        //    return _mapper.Map<List<Competition>>(await _context.Competitions.ToListAsync());
        //}

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<bool> AddUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
         await   _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
             _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

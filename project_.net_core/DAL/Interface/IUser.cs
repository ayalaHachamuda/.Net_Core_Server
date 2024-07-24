using DAL.Dtos;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IUser
    {
        Task<UserDto> GetUser(int id);
        Task<List<UserDto>> GetAllUsers();
        Task<bool>AddUser(UserDto user);
        Task<bool>UpdateUser(UserDto user, int userId);
        Task<bool> DeleteUser(int id);

    }
}

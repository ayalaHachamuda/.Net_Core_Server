using BL.Interface;
using DAL.Data;
using DAL.Dtos;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUser _userData;
        public UserService(IUser userData)
        {
            _userData = userData;
        }
       public async Task<UserDto> GetUser(int id)
        {
            return await _userData.GetUser(id);
        }
        public async Task<bool> AddUser(UserDto user)
        {
            return await _userData.AddUser(user);
        }
        public async Task<bool> UpdateUser(UserDto user)
        {
            return await _userData.UpdateUser(user);
        }
        public async Task<bool> DeleteUser(int id)
        {
            return await _userData.DeleteUser(id);
        }

    }
}

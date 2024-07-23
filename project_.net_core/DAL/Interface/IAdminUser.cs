using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Dtos;
namespace DAL.Interface
{
    public interface IAdminUser
    {
        Task<AdminUserDto> GetAdminUser(int id);
        //Task<List<AdminUserDto>> getAllAdminUsers();
        Task<bool> AddAdminUser(AdminUserDto adminUserDto);
        Task<bool> UpdateAdminUser(AdminUserDto adminUserDto);
        Task<bool> DeleteAdminUser(int id);
    }
}

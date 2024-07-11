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
        Task<AdminUserDto> getAdminUser(int id);
        //Task<List<AdminUserDto>> getAllAdminUsers();
        //Task<bool> addAdminUsern(AdminUser adminUser);
        //Task<bool> putAdminUser(AdminUser adminUser);
        //Task<bool> deleteAdminUser(int id);
    }
}

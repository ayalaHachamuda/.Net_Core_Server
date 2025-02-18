﻿using Models.Models;
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
        Task<List<AdminUserDto>> GetAllAdminUsers();
        Task<bool> AddAdminUser(AdminUserDto adminUserDto);
        Task<bool> UpdateAdminUser(AdminUserDto adminUserDto,int adminId);
        Task<bool> DeleteAdminUser(int id);
    }
}

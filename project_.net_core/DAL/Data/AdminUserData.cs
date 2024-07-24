using AutoMapper;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Interface;
using DAL.Dtos;
namespace DAL.Data
{
    public class AdminUserData : IAdminUser
    {

        private readonly ProjectContext _context;
        private readonly IMapper _mapper;

        public AdminUserData(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<AdminUserDto>> GetAllAdminUsers()
        {
            var adminUsers = await _context.AdminUsers.Where(x => x.Role == "Admin").ToListAsync();
            return _mapper.Map<List<AdminUserDto>>(adminUsers);
        }

        public async Task<AdminUserDto> GetAdminUser(int id)
        {
            var adminUser = await _context.AdminUsers.FindAsync(id);
            if (adminUser == null)
            {
                return null;
            }
            return _mapper.Map<AdminUserDto>(adminUser);
        }

        public async Task<bool> AddAdminUser(AdminUserDto adminUserDto)
        {

            var adminUser = _mapper.Map<AdminUser>(adminUserDto);

            // הגדרת התפקיד כאן
            adminUser.Role = "Admin";

            await _context.AdminUsers.AddAsync(adminUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAdminUser(AdminUserDto adminUserDto, int adminId)
        {
            var adminUser = _mapper.Map<AdminUser>(adminUserDto);
            // הגדרת התפקיד כאן
            adminUser.Role = "Admin";
            adminUser.Id = adminId;
            _context.AdminUsers.Update(adminUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAdminUser(int id)
        {
            var adminUser = await _context.AdminUsers.FindAsync(id);
            if (adminUser != null)
            {
                _context.AdminUsers.Remove(adminUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

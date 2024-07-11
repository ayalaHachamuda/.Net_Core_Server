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
    public class AdminUserData:IAdminUser
    {
        private readonly ProjectContext _context;
        private readonly IMapper _mapper;
        public AdminUserData(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AdminUserDto> getAdminUser(int id)
        {
            var adminUser = await _context.AdminUsers.FindAsync(id);
            return _mapper.Map < AdminUserDto > (adminUser);
        }
        //public async Task<List<AdminUserDto>> getAllAdminUsers()
        //{
        //    var adminUsers = await _context.AdminUsers.ToListAsync();

        //    return _mapper.Map<List<AdminUserDto>>(adminUsers);
        //}
    }
}

using BL.Interface;
using DAL.Interface;
using DAL.Dtos;
using System.Threading.Tasks;

namespace BL.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUser _adminUserRepo;

        public AdminUserService(IAdminUser adminUserRepo)
        {
            _adminUserRepo = adminUserRepo;
        }

        public async Task<AdminUserDto> GetAdminUser(int id)
        {
            return await _adminUserRepo.GetAdminUser(id);
        }
    }
}

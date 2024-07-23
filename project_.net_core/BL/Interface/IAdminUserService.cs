using System.Threading.Tasks;
using DAL.Dtos;

namespace BL.Interface
{
    public interface IAdminUserService
    {
        Task<AdminUserDto> GetAdminUser(int id);
    }
}

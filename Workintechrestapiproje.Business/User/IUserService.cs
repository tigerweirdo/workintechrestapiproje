using System.Collections.Generic;
using System.Threading.Tasks;
using Workintechrestapiproje.Domain;

namespace Workintechrestapiproje.Business.Users
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
    }
}

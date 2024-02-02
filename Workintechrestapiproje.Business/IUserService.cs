using Workintechrestapiproje.Domain;

namespace Workintechrestapiproje.Business
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
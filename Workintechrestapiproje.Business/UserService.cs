using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workintechrestapiproje.Domain;

namespace Workintechrestapiproje.Business
{
    public class UserService : IUserService
    {
        private static readonly List<User> users = new();
        private static int nextId = 1;

        public Task<List<User>> GetAllUsersAsync()
        {
            return Task.FromResult(users);
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<User> AddUserAsync(User user)
        {
            user.Id = nextId++;
            users.Add(user);
            return Task.FromResult(user);
        }

        public Task UpdateUserAsync(User user)
        {
            var index = users.FindIndex(u => u.Id == user.Id);
            if (index != -1)
            {
                users[index] = user;
            }
            return Task.CompletedTask;
        }

        public Task DeleteUserAsync(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
            }
            return Task.CompletedTask;
        }
    }
}

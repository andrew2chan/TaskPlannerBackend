using TaskPlanner.DTOs;
using TaskPlanner.Models;

namespace TaskPlanner.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool CreateUser(User user);
        bool DeleteUser(User user);
        bool UpdateUser(User user);
        bool UserExists(int id);
        bool Save();
    }
}

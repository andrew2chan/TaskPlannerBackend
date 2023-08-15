using TaskPlanner.DTOs;
using TaskPlanner.Models;

namespace TaskPlanner.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string email);
        bool CreateUser(User user);
        bool DeleteUser(User user);
        bool UpdateUser(User user);
        bool UserExists(int id);
        bool UserExists(string email);
        bool Save();
    }
}

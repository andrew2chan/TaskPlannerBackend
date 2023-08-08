using SQLitePCL;
using TaskPlanner.Context;
using TaskPlanner.DTOs;
using TaskPlanner.Interfaces;
using TaskPlanner.Models;

namespace TaskPlanner.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            Save();

            var newUser = _context.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            var newPlannedTask = new PlannedTasks() //create a new planned task for each new user
            {
                UserId = newUser.Id,
                User = newUser
            };

            _context.PlannedTasks.Add(newPlannedTask);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}

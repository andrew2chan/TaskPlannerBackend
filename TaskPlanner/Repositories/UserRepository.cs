﻿using SQLitePCL;
using TaskPlanner.Context;
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

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}

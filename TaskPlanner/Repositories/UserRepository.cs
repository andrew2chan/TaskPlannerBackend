﻿using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using TaskPlanner.Context;
using TaskPlanner.DTOs;
using TaskPlanner.Helper;
using TaskPlanner.Interfaces;
using TaskPlanner.Models;

namespace TaskPlanner.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly LoginHelper _loginHelper;

        public UserRepository(DataContext context)
        {
            _context = context;
            _loginHelper = new LoginHelper();
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

        public User GetUser(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
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

        public bool UpdateUser(int userId, UserDto userdto)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            string salt = _loginHelper.GenerateSalt();
            string hashedPassword = _loginHelper.HashPassword(userdto.Password, salt);

            user.Email = userdto.Email;
            user.Name = userdto.Name;

            if (userdto.Password.Length > 0) //if password needs to be updated
            {
                user.Salt = salt;
                user.HashedPassword = hashedPassword;
                //user.Password = userdto.Password;
            }

            //_context.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}

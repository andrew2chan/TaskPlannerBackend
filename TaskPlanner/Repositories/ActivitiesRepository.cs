using SQLitePCL;
using TaskPlanner.Context;
using TaskPlanner.Interfaces;
using TaskPlanner.Models;

namespace TaskPlanner.Repositories
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly DataContext _context;

        public ActivitiesRepository(DataContext context)
        {
            _context = context;
        }

        public bool ActivitiesExists(int id)
        {
            return _context.Activities.Any(a => a.Id == id);
        }

        public bool CreateActivity(Activities activity)
        {
            _context.Add(activity);
            return Save();
        }

        public bool DeleteActivity(Activities activity)
        {
            _context.Remove(activity);
            return Save();
        }

        public ICollection<Activities> GetActivitiesById(int id)
        {
            return _context.Activities.Where(a => a.Id == id).ToList();
        }

        public ICollection<Activities> GetActivitiesByUserId(int userId)
        {
            var plannedTasks = _context.PlannedTasks.Where(pt => pt.UserId == userId).FirstOrDefault();
            var activities = _context.Activities.Where(a => a.PlannedTasksId == plannedTasks.Id).ToList();

            return activities;
        }

        public ICollection<Activities> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

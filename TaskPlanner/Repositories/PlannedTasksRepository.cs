using SQLitePCL;
using TaskPlanner.Context;
using TaskPlanner.Interfaces;
using TaskPlanner.Models;

namespace TaskPlanner.Repositories
{
    public class PlannedTasksRepository : IPlannedTasksRepository
    {
        private readonly DataContext _context;

        public PlannedTasksRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<PlannedTasks> GetAllPlannedTasks()
        {
            return _context.PlannedTasks.ToList();
        }

        public PlannedTasks GetPlannedTaskByUserId(int userId)
        {
            return _context.PlannedTasks.Where(pt => pt.UserId == userId).FirstOrDefault();
        }
    }
}

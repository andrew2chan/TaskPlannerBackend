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
    }
}

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

        public ICollection<Activities> GetActivitiesById(int id)
        {
            return _context.Activities.Where(a => a.Id == id).ToList();
        }

        public ICollection<Activities> GetAllActivities()
        {
            return _context.Activities.ToList();
        }
    }
}

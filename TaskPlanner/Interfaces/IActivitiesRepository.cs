using TaskPlanner.Models;

namespace TaskPlanner.Interfaces
{
    public interface IActivitiesRepository
    {
        ICollection<Activities> GetAllActivities();
        ICollection<Activities> GetActivitiesById(int id);
        bool ActivitiesExists(int id);
    }
}

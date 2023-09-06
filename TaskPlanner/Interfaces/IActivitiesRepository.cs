using TaskPlanner.Models;

namespace TaskPlanner.Interfaces
{
    public interface IActivitiesRepository
    {
        ICollection<Activities> GetAllActivities();
        ICollection<Activities> GetActivitiesById(int id);
        ICollection<Activities> GetActivitiesByUserId(int userId);
        bool DeleteActivity(Activities activity);
        bool CreateActivity(Activities activity);
        bool UpdateActivity(Activities activity);
        bool ActivitiesExists(int id);
        bool Save();
    }
}

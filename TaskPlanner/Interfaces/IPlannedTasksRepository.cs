using TaskPlanner.Models;

namespace TaskPlanner.Interfaces
{
    public interface IPlannedTasksRepository
    {
        ICollection<PlannedTasks> GetAllPlannedTasks();
        PlannedTasks GetPlannedTaskByUserId(int userId);
    }
}

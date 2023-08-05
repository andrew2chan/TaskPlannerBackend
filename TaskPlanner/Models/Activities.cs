namespace TaskPlanner.Models
{
    public class Activities
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public DateTime ActivityStartTime { get; set; }
        public DateTime ActivityEndTime { get; set; }
        public int PlannedTasksId { get; set; }
        public PlannedTasks PlannedTasks { get; set; }
    }
}

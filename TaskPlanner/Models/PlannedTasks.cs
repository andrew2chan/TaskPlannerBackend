namespace TaskPlanner.Models
{
    public class PlannedTasks
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Activities> Activities { get; set; }
    }
}

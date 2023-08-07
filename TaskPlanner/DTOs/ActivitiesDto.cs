﻿namespace TaskPlanner.DTOs
{
    public class ActivitiesDto
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public DateTime ActivityStartTime { get; set; }
        public DateTime ActivityEndTime { get; set; }
        public int PlannedTasksId { get; set; }
    }
}

using TaskPlanner.Context;
using TaskPlanner.Models;

namespace TaskPlanner
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if(!dataContext.PlannedTasks.Any())
            {
                var plannedTasks = new List<PlannedTasks>()
                {
                    new PlannedTasks()
                    {
                        UserId = 1,
                        User = new User()
                        {
                            Name = "Bob",
                            Email = "Bob@gmail.com",
                            Password = "password",
                            HashedPassword = "a234adf3",
                            Salt = "a323asdf3dsf"
                        },
                        Activities = new List<Activities>()
                        {
                            new Activities {
                                ActivityName = "Soccer",
                                ActivityStartTime = new DateTime(2023, 2, 3, 7, 20, 0),
                                ActivityEndTime = new DateTime(2023, 2, 3, 8, 30, 0),
                                PlannedTasksId = 1
                            },
                            new Activities {
                                ActivityName = "Gym",
                                ActivityStartTime = new DateTime(2023, 2, 3, 8, 50, 0),
                                ActivityEndTime = new DateTime(2023, 2, 3, 9, 20, 0),
                                PlannedTasksId = 1
                            },
                        }
                    }
                };

                dataContext.PlannedTasks.AddRange(plannedTasks);
                dataContext.SaveChanges();
            }
        }
    }
}

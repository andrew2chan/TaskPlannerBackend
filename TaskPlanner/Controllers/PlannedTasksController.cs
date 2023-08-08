using Microsoft.AspNetCore.Mvc;
using TaskPlanner.Interfaces;

namespace TaskPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannedTasksController : ControllerBase
    {
        private readonly IPlannedTasksRepository _plannedTasksRepository;

        public PlannedTasksController(IPlannedTasksRepository plannedTasksRepository)
        {
            _plannedTasksRepository = plannedTasksRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetPlannedTasks()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_plannedTasksRepository.GetAllPlannedTasks());
        }
    }
}

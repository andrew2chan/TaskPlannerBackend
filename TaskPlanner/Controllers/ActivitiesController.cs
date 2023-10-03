using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPlanner.DTOs;
using TaskPlanner.Interfaces;
using TaskPlanner.Models;

namespace TaskPlanner.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlannedTasksRepository _plannedTasksRepository;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivitiesRepository activiiesRepository, IUserRepository userRepository, IPlannedTasksRepository plannedTasksRepository, IMapper mapper)
        {
            _activitiesRepository = activiiesRepository;
            _userRepository = userRepository;
            _plannedTasksRepository = plannedTasksRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllActivities()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var allActivities = _activitiesRepository.GetAllActivities();

            return Ok(allActivities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetActivitiesById(int id)
        {
            if (!_activitiesRepository.ActivitiesExists(id))
                return NotFound();

            var activitiesById = _mapper.Map<List<ActivitiesDto>>(_activitiesRepository.GetActivitiesById(id));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(activitiesById);
        }

        [HttpGet("userActivity/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetActivitiesByUserId(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var activitiesByUserId = _mapper.Map<List<ActivitiesDto>>(_activitiesRepository.GetActivitiesByUserId(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(activitiesByUserId);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateActivity([FromQuery] int userId, [FromBody] ActivitiesDto activity)
        {

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (activity == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var plannedTask = _plannedTasksRepository.GetAllPlannedTasks().Where(pt => pt.UserId == userId).FirstOrDefault();

            var activityMap = _mapper.Map<Activities>(activity); //map to activities from activitesDTO
            activityMap.PlannedTasksId = plannedTask.Id; //fill in the foreign key
            
            activityMap.ActivityStartTime = activityMap.ActivityStartTime.ToUniversalTime();
            activityMap.ActivityEndTime = activityMap.ActivityEndTime.ToUniversalTime();

            if (!_activitiesRepository.CreateActivity(activityMap))
            {
                ModelState.AddModelError("", "Something went wrong with creating an activity");
                return StatusCode(500, ModelState);
            }

            return Ok("Created activity");
        }

        [HttpDelete("{activityId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteActivity(int activityId)
        {
            if (!_activitiesRepository.ActivitiesExists(activityId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var activity = _activitiesRepository.GetActivitiesById(activityId).FirstOrDefault();

            if(!_activitiesRepository.DeleteActivity(activity))
            {
                ModelState.AddModelError("", "Something went wrong with deleting activity");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleted activity!");
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateActivity([FromBody] ActivitiesDto activity)
        {
            if (!_activitiesRepository.ActivitiesExists(activity.Id))
                return NotFound();

            if(activity == null)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var activityMap = _mapper.Map<Activities>(activity); //map to activities from activitesDTO
            
            activityMap.ActivityStartTime = activityMap.ActivityStartTime.ToUniversalTime();
            activityMap.ActivityEndTime = activityMap.ActivityEndTime.ToUniversalTime();

            activityMap.PlannedTasksId = activity.PlannedTasksId;
            activityMap.PlannedTasks = _plannedTasksRepository.GetAllPlannedTasks().Where(pt => pt.Id == activity.PlannedTasksId).FirstOrDefault();

            if (!_activitiesRepository.UpdateActivity(activityMap))
            {
                ModelState.AddModelError("", "Something went wrong with updating activity");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated activity!");
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskPlanner.DTOs;
using TaskPlanner.Interfaces;

namespace TaskPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesRepository _activitiesRepository;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivitiesRepository activiiesRepository, IMapper mapper)
        {
            _activitiesRepository = activiiesRepository;
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
    }
}

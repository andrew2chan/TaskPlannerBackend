using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TaskPlanner.DTOs;
using TaskPlanner.Interfaces;
using TaskPlanner.Models;

namespace TaskPlanner.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(id));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            if(user == null)
                return BadRequest(ModelState);

            if (ValidateEmail(user))
            {
                ModelState.AddModelError("", "Please enter a proper email address.");
                return BadRequest(ModelState);
            }

            if (ValidatePassword(user) == false)
            {
                ModelState.AddModelError("", "Please make sure to enter a valid password.");
                return BadRequest(ModelState);
            }

            if (ValidateName(user) == false)
            {
                ModelState.AddModelError("", "Please make sure to enter a valid name.");
                return BadRequest(ModelState);
            }

            var existingUsers = _userRepository.GetUsers().Where(u => u.Email.Trim().ToUpper() == user.Email.Trim().ToUpper()).FirstOrDefault();

            if(existingUsers != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = _mapper.Map<User>(user);
            newUser.HashedPassword = "placeholder hashed pass";
            newUser.Salt = "placeholder salt";

            if(!_userRepository.CreateUser(newUser))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Created!");
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if(!_userRepository.UserExists(userId))
                return BadRequest(ModelState);

            var user = _userRepository.GetUser(userId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong with deleting");
            }

            return Ok("Deleted!");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if(!_userRepository.UserExists(userId))
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(updatedUser == null)
                return BadRequest(ModelState);

            var upUser = _mapper.Map<User>(updatedUser);
            upUser.HashedPassword = "placeholder updated pass";
            upUser.Salt = "placeholder updated salt";

            if(!_userRepository.UpdateUser(upUser))
            {
                ModelState.AddModelError("", "Something went wrong with updating");
            }

            return NoContent();
        }

        private bool ValidateEmail(UserDto user)
        {
            var email = user.Email; //gets email from userDTO
            String pattern = @"^[A-Z0-9+_.-]+@[A-Z0-9-]+[.][A-Z]+$"; //checks this pattern X@X.c

            return !Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private bool ValidatePassword(UserDto user)
        {
            var password = user.Password;

            var passwordIsValid = true;

            String pattern = @"\s{1,}";

            if (password.Length == 0 || Regex.IsMatch(password, pattern, RegexOptions.IgnoreCase)) 
                passwordIsValid = false;

            return passwordIsValid;
        }

        private bool ValidateName(UserDto user)
        {
            var name = user.Name;

            var nameIsValid = false;

            String pattern = @"[A-Z]{1,}";

            if (Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase))
                nameIsValid = true;

            return nameIsValid;
        }
    }
}

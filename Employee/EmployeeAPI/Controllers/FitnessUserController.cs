using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.BuisnessLogic;
namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessUserController : ControllerBase
    {
        private readonly FitnessUserService _users;

        public FitnessUserController(FitnessUserService users)
        {
            _users = users;
        }

        // POST: api/FitnessUsers
        [HttpPost("AddFitnessUser")]
        public IActionResult AddFitnessUser([FromBody] FitnessUser user)
        {
            if (user == null)
                return BadRequest("User data is required.");

            _users.AddFitnessUser(user);
            return Ok("User added successfully.");
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            bool isAuthenticated = _users.AuthenticateUser(request.Email, request.Password);

            if (isAuthenticated)
            {   

                return Ok(new { message = "Login successful" });
            }

            return Unauthorized(new { message = "Invalid email or password" });
        }

        [HttpGet("GetFitnessUsers")]
        public IActionResult GetFitnessUsers()
        {
            var users = _users.GetFitnessUsers();
            if (users == null || !users.Any())
                return NotFound("No users found.");

            return Ok(users);
        }
        [HttpGet("GetExercises")]
        public IActionResult GetExercises()
        {
            var exercises = _users.GetExercises();
            if (exercises == null || !exercises.Any())
                return NotFound("No exercises found.");

            return Ok(exercises);
        }

        [HttpGet("{fitnessUserId}")]
        public IActionResult GetUserExercises(int fitnessUserId)
        {
            var exercises = _users.GetUserExercises(fitnessUserId);

            if (exercises == null || exercises.Count == 0)
            {
                return NotFound(new { message = "No exercises found for this user." });
            }

            return Ok(exercises);
        }
    

    public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

    }
}

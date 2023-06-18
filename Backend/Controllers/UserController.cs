using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Interface;
using System;
using System.Threading.Tasks;
using BusinessLogic.Models.User;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/user
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        // GET: api/user
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel newUser)
        {
            if (ModelState.IsValid)
            {
                // Verify if email already exist
                var existingEmailUser = await _userRepository.GetUserByEmail(newUser.Email);
                if (existingEmailUser != null)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return BadRequest(ModelState);
                }

                // Verify if phone already exist
                var existingPhoneUser = await _userRepository.GetUserByPhone(newUser.Phone);
                if (existingPhoneUser != null)
                {
                    ModelState.AddModelError("Phone", "Phone number already exists");
                    return BadRequest(ModelState);
                }

                // Verify if username already exist
                var existingUsernameUser = await _userRepository.GetUserByUsername(newUser.Username);
                if (existingUsernameUser != null)
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return BadRequest(ModelState);
                }

                var createdUser = await _userRepository.CreateUser(newUser);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.ID }, createdUser);
            }

            return BadRequest(ModelState);
        }

        // POST: api/user
        [HttpPost("registration")]
        public async Task<IActionResult> UserRegistration([FromBody] CreateUserModel newUser)
        {
            if (ModelState.IsValid)
            {
                // Verify if email already exist
                var existingEmailUser = await _userRepository.GetUserByEmail(newUser.Email);
                if (existingEmailUser != null)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return BadRequest(ModelState);
                }

                // Verify if phone already exist
                var existingPhoneUser = await _userRepository.GetUserByPhone(newUser.Phone);
                if (existingPhoneUser != null)
                {
                    ModelState.AddModelError("Phone", "Phone number already exists");
                    return BadRequest(ModelState);
                }

                // Verify if username already exist
                var existingUsernameUser = await _userRepository.GetUserByUsername(newUser.Username);
                if (existingUsernameUser != null)
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return BadRequest(ModelState);
                }

                var createdUser = await _userRepository.CreateUser(newUser);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.ID }, createdUser);
            }

            return BadRequest(ModelState);
        }
        
        // PUT: api/user/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserModel updatedUser)
        {
            if (id != updatedUser.ID)
            {
                return BadRequest("User Not Found!");
            }

            var existingUser = await _userRepository.GetUser(id);

            if (existingUser == null)
            {
                return NotFound("User Not Found!");
            }

            var existingUserByEmail = await _userRepository.GetUserByEmail(updatedUser.Email);

            if (existingUserByEmail != null && existingUserByEmail.ID != id)
            {
                return Conflict("Email already exists!");
            }

            var existingUserByPhone = await _userRepository.GetUserByPhone(updatedUser.Phone);

            if (existingUserByPhone != null && existingUserByPhone.ID != id)
            {
                return Conflict("Phone number already exists!");
            }

            var existingUserByUsername = await _userRepository.GetUserByUsername(updatedUser.Username);

            if (existingUserByUsername != null && existingUserByUsername.ID != id)
            {
                return Conflict("Username already exists!");
            }

            await _userRepository.UpdateUser(updatedUser);

            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userRepository.DeleteUser(id);

            return NoContent();
        }
        
                
        // GET: api/user
        [HttpGet("GetRoles")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetRole()
        {
            var roles = await _userRepository.GetRole();
            return Ok(roles);
        }
    }
}

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
        [HttpGet]
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
    }
}

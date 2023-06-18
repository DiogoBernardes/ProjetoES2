using Backend.Interface;
using BusinessLogic.Models.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRegistController : ControllerBase
    {
        private readonly IEventRegistRepository _eventRegistRepository;

        public EventRegistController(IEventRegistRepository eventRegistRepository)
        {
            _eventRegistRepository = eventRegistRepository;
        }

        // GET: api/EventsRegist
        [HttpGet]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetEventsRegist()
        {
            var getRegists = await _eventRegistRepository.GetEventsRegist();
            return Ok(getRegists);
         
        }

        // GET: api/EventRegist
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetEventRegist(Guid id)
        {
            var getRegist = await _eventRegistRepository.GetEventRegist(id);

            if (getRegist == null)
            {
                return NotFound();
            }

            return Ok(getRegist);
        }

        // POST: api/EventRegist
        [HttpPost]
        [Authorize(Roles = "Admin, UserManager,Users")]
        public async Task<IActionResult> CreateEventRegist([FromBody] EventRegistModel newRegist)
        {
            if (ModelState.IsValid)
            {
                var createdRegist = await _eventRegistRepository.CreateEventRegist(newRegist);
                return Ok(createdRegist);
            }

            return BadRequest("Something went wrong!!");
        }
        
        // PUT: api/EventRegist/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] EventRegistModel updatedRegist)
        { 
            if (id != updatedRegist.ID)
            {
                return BadRequest("Registration Not Found!");
            }

            await _eventRegistRepository.UpdateEventRegist(updatedRegist);

            return NoContent();
        }
        
        
        // DELETE: api/EventRegist/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            int registDeletedId = await _eventRegistRepository.DeleteEventRegist(id);

            if (registDeletedId == 200)
            {
                return Ok();
            }
            
            return NoContent();
        }
        
    }
}
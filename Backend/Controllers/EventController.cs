using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Interface;
using BusinessLogic.Models.Event;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET: api/Event
        [HttpGet]
        [Authorize(Roles = "Admin, UserManager,Users")]
        public async Task<IActionResult> GetEvents()
        {
            var getEvents = await _eventRepository.GetEvents();
            return Ok(getEvents);
         
        }

        // GET: api/Event
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,UserManager,Users")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var getEvent = await _eventRepository.GetEvent(id);

            if (getEvent == null)
            {
                return NotFound();
            }

            return Ok(getEvent);
        }

        // POST: api/Event
        [HttpPost("add")]
        [Authorize(Roles = "Admin,UserManager,Users")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventModel newEvent)
        {
            DateTime nowPlus10Minutes = DateTime.Now.AddMinutes(10);

            if (newEvent.Date_Hour < nowPlus10Minutes)
            {
                return BadRequest("The event date cannot be earlier than the current day plus 10 minutes.");
            }

            if (ModelState.IsValid)
            {
                var createdEvent = await _eventRepository.CreateEvent(newEvent);
                return Ok(createdEvent);
            }

            return BadRequest("Something went wrong!!");
        }
        
        // PUT: api/Event/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,UserManager,Users")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventModel updatedEvent)
        { 
            if (id != updatedEvent.ID)
            {
                return BadRequest("User Not Found!");
            }

            await _eventRepository.UpdateEvent(updatedEvent);

            return NoContent();
        }
        
        
        // DELETE: api/Event/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,UserManager,Users")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            int eventDeletedId = await _eventRepository.DeleteEvent(id);

            if (eventDeletedId == 200)
            {
                return Ok();
            }
            
            return NoContent();
        }
    
              
    }
}

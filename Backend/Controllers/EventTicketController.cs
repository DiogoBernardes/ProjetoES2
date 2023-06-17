using Backend.Interface;
using BusinessLogic.Models.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTicketController : ControllerBase
    {
        private readonly IEventTicketRepository _eventTicketRepository;

        public EventTicketController(IEventTicketRepository eventTicketRepository)
        {
            _eventTicketRepository = eventTicketRepository;
        }

        // GET: api/user
        [HttpGet]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetEventTickets()
        {
            var eventTicket = await _eventTicketRepository.GetEventTickets();
            return Ok(eventTicket);

        }

        // GET: api/user
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetEventTicket(Guid id)
        {
            var eventTicket = await _eventTicketRepository.GetEventTicket(id);

            if (eventTicket == null)
            {
                return NotFound();
            }

            return Ok(eventTicket);
        }

        // POST: api/user
        [HttpPost]
        [Authorize(Roles = "Admin, UserManager,Users")]
        public async Task<IActionResult> CreateEventTicket([FromBody] EventTicketModel newEventTicket)
        {
            if (ModelState.IsValid)
            {
                var createdEventTicket = await _eventTicketRepository.CreateEventTicket(newEventTicket);
                return Ok(createdEventTicket);
            }

            return BadRequest("Something went wrong!!");
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEventTicket(Guid id, [FromBody] EventTicketModel updatedEventTicket)
        {
            if (id != updatedEventTicket.ID)
            {
                return BadRequest("Event Ticket Not Found!");
            }

            await _eventTicketRepository.UpdateEventTicket(updatedEventTicket);

            return NoContent();
        }


        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEventTicket(Guid id)
        {
            int eventTicketDeletedId = await _eventTicketRepository.DeleteEventTicket(id);

            if (eventTicketDeletedId == 200)
            {
                return Ok();
            }

            return NoContent();
        }
    }
}
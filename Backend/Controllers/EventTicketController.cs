using Backend.Interface;
using BusinessLogic.Models.Event;
using BusinessLogic.Models.Event.ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTicketController : ControllerBase
    {
        private readonly IEventTicketRepository _eventTicketRepository;
        private readonly IEventRepository _eventRepository;
        
        public EventTicketController(IEventTicketRepository eventTicketRepository, IEventRepository eventRepository, IEventTicketRepository ticketTypeRepository)
        {
            _eventRepository = eventRepository;
            _eventTicketRepository = eventTicketRepository;
        }

        // GET: api/EventTicket
        [HttpGet]
        [Authorize(Roles = "Admin, UserManager,User")]
        public async Task<IActionResult> GetEventTickets()
        {
            var eventTicket = await _eventTicketRepository.GetEventTickets();
            return Ok(eventTicket);

        }

        // GET: api/EventTicket
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, UserManager,User")]
        public async Task<IActionResult> GetEventTicket(Guid id)
        {
            var eventTicket = await _eventTicketRepository.GetEventTicket(id);

            if (eventTicket == null)
            {
                return NotFound();
            }

            return Ok(eventTicket);
        }
        
        // GET: api/EventTicket/GetEventTicketsByEvent/{eventId}
        [HttpGet("GetEventTicketsByEvent/{eventId}")]
        [Authorize(Roles = "Admin, UserManager,User")]
        public async Task<IActionResult> GetEventTicketsByEvent(Guid eventId)
        {
            var eventTickets = await _eventTicketRepository.GetEventTicketsByEvent(eventId);
            return Ok(eventTickets);
        }

        // POST: api/EventTicket
        [HttpPost]
        [Authorize(Roles = "Admin, UserManager, Users")]
        public async Task<IActionResult> CreateEventTicket([FromBody] CreateEventTicketModel newEventTicket)
        {
            if (ModelState.IsValid)
            {
                // Verificar a capacidade do evento
                var getEv = await _eventRepository.GetEvent(newEventTicket.event_ID);
                var eventCapacity = getEv.Capacity;
                
                if (eventCapacity == 0)
                {
                    return BadRequest("Event is full.");
                }

                // Verificar se o tipo de bilhete já está associado ao evento
                var existingTicketType = await _eventTicketRepository.GetEventTicketByType(newEventTicket.event_ID, newEventTicket.ticket_ID);
                if (existingTicketType != null)
                {
                    return BadRequest("Ticket type already associated with the event.");
                }

                var createdEventTicket = await _eventTicketRepository.CreateEventTicket(newEventTicket);
                return Ok(createdEventTicket);
            }

            return BadRequest("Invalid ticket data.");
        }


        // PUT: api/EventTicket/{id}
        [HttpPut("EditTicket/{id}")]
        [Authorize(Roles = "Admin,UserManager,User")]
        public async Task<IActionResult> UpdateEventTicket(Guid id, [FromBody] EditEventTicketModel updatedEventTicket)
        {
            if (id != updatedEventTicket.ID)
            {
                return BadRequest("Event Ticket Not Found!");
            }

            await _eventTicketRepository.UpdateEventTicket(updatedEventTicket);

            return NoContent();
        }


        // DELETE: api/EventTicket/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,UserManager,User")]
        public async Task<IActionResult> DeleteEventTicket(Guid id)
        {
            int eventTicketDeletedId = await _eventTicketRepository.DeleteEventTicket(id);

            if (eventTicketDeletedId == 200)
            {
                return Ok();
            }

            return NoContent();
        }
        
        // GET: api/EventTicket/GetAvailableTicketTypes
        [HttpGet("GetAvailableTicketTypes")]
        [Authorize(Roles = "Admin, UserManager,User")]
        public async Task<IActionResult> GetAvailableTicketTypes()
        {
            var ticketTypes = await _eventTicketRepository.GetAvailableTicketTypes();
            return Ok(ticketTypes);
        }
    }
    
    
}
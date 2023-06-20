using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRegistController : ControllerBase
    {
        private readonly IEventRegistRepository _eventRegistRepository;
        private readonly ES2DbContext _context;

        public EventRegistController(IEventRegistRepository eventRegistRepository, ES2DbContext context)
        {
            _eventRegistRepository = eventRegistRepository;
            _context = context;
        }

        // GET: api/EventsRegist
        [HttpGet]
        [Authorize(Roles = "Admin, UserManager,User")]
        public async Task<IActionResult> GetEventsRegist()
        {
            var getRegists = await _eventRegistRepository.GetEventsRegist();
            return Ok(getRegists);
         
        }

        [HttpGet("api/EventRegistration/GetAllRegistsOnEvent/{id}")]
        public async Task<List<EventRegistModel>> GetAllRegistsOnEvent(Guid id)
        {
            return await _eventRegistRepository.GetAllRegistsOnEvent(id);
        }
        
        // GET: api/EventRegist
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, UserManager,User")]
        public async Task<IActionResult> GetEventRegist(Guid id)
        {
            var getRegist = await _eventRegistRepository.GetEventRegist(id);

            if (getRegist == null)
            {
                return NotFound();
            }

            return Ok(getRegist);
        }
        
        // GET: api/EventRegist/GetEventRegistByEvent/{eventId}
        [HttpGet("GetEventRegistByEvent/{eventId}")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetEventRegistByEvent(Guid eventId)
        {
            var eventRegistIds = await _eventRegistRepository.GetEventRegistIdsByEvent(eventId);
            return Ok(eventRegistIds);
        }

        // POST: api/EventRegist
        [HttpPost]
        [Authorize(Roles = "Admin, UserManager,User")]
        public async Task<IActionResult> CreateEventRegist([FromBody] EventRegistModel newRegist)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o ID do participante é diferente do ID do organizador do evento
                var eventOrganizerId = await _context.Set<event_info>()
                    .Where(e => e.id == newRegist.Event_ID)
                    .Select(e => e.organizer_id)
                    .FirstOrDefaultAsync();

                if (eventOrganizerId == newRegist.Participant_ID)
                {
                    return BadRequest("The event organizer cannot be an attendee!");
                }
                
                // Verifica se o Participante já está inscrito no evento
                int participantRegist = await _context.Set<event_regist>()
                    .CountAsync(regist => regist.event_id == newRegist.Event_ID && regist.participant_id == newRegist.Participant_ID);

                if (participantRegist > 0)
                {
                    return BadRequest("The user already has a registration for this event!");
                }
                
                var createdRegist = await _eventRegistRepository.CreateEventRegist(newRegist);
                return Ok(createdRegist);
            }

            return BadRequest("Model state is invalid.");
        }

        
        // PUT: api/EventRegist/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,UserManager,User")]
        public async Task<IActionResult> UpdateEventRegist(Guid id, [FromBody] EventRegistModel updatedRegist)
        { 
            if (id != updatedRegist.ID)
            {
                return BadRequest("Registration Not Found!");
            }
            
            // Verificar se o ID do participante é diferente do ID do organizador do evento
            var eventOrganizerId = await _context.Set<event_info>()
                .Where(e => e.id == updatedRegist.Event_ID)
                .Select(e => e.organizer_id)
                .FirstOrDefaultAsync();

            if (eventOrganizerId == updatedRegist.Participant_ID)
            {
                return BadRequest("The event organizer cannot be an attendee!");
            }
            // Verifica se o Participante já está inscrito no evento
            int participantRegist = await _context.Set<event_regist>()
                .CountAsync(regist => regist.event_id == updatedRegist.Event_ID && regist.participant_id == updatedRegist.Participant_ID);

            if (participantRegist > 0)
            {
                return BadRequest("The user already has a registration for this event!");
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
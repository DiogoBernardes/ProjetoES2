using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ES2DbContext _context;

        public MessageController(IMessageRepository messageRepository, ES2DbContext context)
        {
            _messageRepository = messageRepository;
            _context = context;
        }
        
        // GET: api/Message
        [HttpGet("organizer/{id}")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetOrganizerMessages(Guid id)
        {
            var getMessages = await _messageRepository.GetOrganizerMessages(id);

            if (getMessages == null)
            {
                return NotFound();
            }

            return Ok(getMessages);
        }

        // GET: api/Message
        [HttpGet("participant/{id}")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetParticipantMessages(Guid id)
        {
            var getMessages = await _messageRepository.GetParticipantMessages(id);

            if (getMessages == null)
            {
                return NotFound();
            }

            return Ok(getMessages);
        }

        // POST: api/CreateMessage
        [HttpPost]
        [Authorize(Roles = "Admin, UserManager,Users")]
        public async Task<IActionResult> CreateMessage([FromBody] MessagesModel newMessage)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o organizer do evento é o organizer da mensagem
                var eventOrganizerId = await _context.Set<event_info>()
                    .Where(e => e.id == newMessage.Event_ID)
                    .Select(e => e.organizer_id)
                    .FirstOrDefaultAsync();

                if (eventOrganizerId != newMessage.Organizer_ID)
                {
                    return BadRequest("The organizer you are sending the message to is not the event organizer!");
                }
                
                // Verificar se o organizer do evento é o organizer da mensagem
                var eventParticipantId = await _context.Set<event_regist>()
                    .Where(e => e.event_id == newMessage.Event_ID)
                    .Select(e => e.participant_id)
                    .FirstOrDefaultAsync();
                
                if (eventParticipantId != newMessage.Participant_ID)
                {
                    return BadRequest("The user you are sending the message to is not an event attendee!");
                }
                
                var createdMessage = await _messageRepository.CreateMessage(newMessage);
                return Ok(createdMessage);
            }

            return BadRequest("Something went wrong!!");
        }
        
        
        
    }
}
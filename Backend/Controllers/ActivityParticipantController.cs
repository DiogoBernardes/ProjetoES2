using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Activity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityParticipantController : ControllerBase
    {

        private readonly IActivityParticipantRepository _activityParticipantRepository;
        private readonly ES2DbContext _context;

        public ActivityParticipantController(IActivityParticipantRepository activityParticipantRepository, ES2DbContext context)
        {
            _activityParticipantRepository = activityParticipantRepository;
            _context = context;
        }

        // GET: api/ActivityParticipant
        [HttpGet]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetActivitiesParticipants()
        {
            var getActivitiesRegists = await _activityParticipantRepository.GetActivitiesParticipants();
            return Ok(getActivitiesRegists);
         
        }

        // GET: api/ActivityParticipant
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetActivityParticipant(Guid id)
        {
            var getActivitiesRegist = await _activityParticipantRepository.GetActivityParticipant(id);

            if (getActivitiesRegist == null)
            {
                return NotFound("Registration on activity Not Found!");
            }

            return Ok(getActivitiesRegist);
        }

        // POST: api/ActivityParticipant
        [HttpPost]
        [Authorize(Roles = "Admin, UserManager,Users")]
        public async Task<IActionResult> CreateActivityParticipant([FromBody] ActivityParticipantModel newActivityRegist)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o ID do participante é diferente do ID do organizador do evento
                var eventOrganizerId = await _context.Set<activity_info>()
                    .Where(e => e.id == newActivityRegist.Activity_ID)
                    .Select(e => e._event.organizer_id)
                    .FirstOrDefaultAsync();

                if (eventOrganizerId == newActivityRegist.Participant_ID)
                {
                    return BadRequest("The event organizer cannot participate in the activities of his own event!");
                }
                
                // Verifica se o Participante está inscrito no evento
                var isRegistedOnEvent = await _context.Set<activity_info>()
                    .Where(e => e.id == newActivityRegist.Activity_ID)
                    .SelectMany(e => e._event.event_regists)
                    .FirstOrDefaultAsync(er => er.participant_id == newActivityRegist.Participant_ID);

                if (isRegistedOnEvent == null)
                {
                    return BadRequest("The user hasn't a registration for the event where this activity belong!");
                }

                // Verifica se o Participante já está inscrito na atividade
                int participantRegist = await _context.Set<activity_participant>()
                    .CountAsync(regist => regist.activity_id == newActivityRegist.Activity_ID 
                                          && regist.participant_id == newActivityRegist.Participant_ID);

                if (participantRegist > 0)
                {
                    return BadRequest("The user already has a registration for this activity!");
                }

                
                var createdActivityParticipant = await _activityParticipantRepository.CreateActivityParticipant(newActivityRegist);
                return Ok(createdActivityParticipant);
            }

            return BadRequest("Something went wrong!!");
        }
        
        // PUT: api/ActivityParticipant/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateActivityParticipant(Guid id, [FromBody] ActivityParticipantModel updatedActivityRegist)
        { 
            if (id != updatedActivityRegist.ID)
            {
                return BadRequest("Registration on activity Not Found!");
            }
            
            // Verificar se o ID do participante é diferente do ID do organizador do evento
            var eventOrganizerId = await _context.Set<activity_info>()
                .Where(e => e.id == updatedActivityRegist.Activity_ID)
                .Select(e => e._event.organizer_id)
                .FirstOrDefaultAsync();

            if (eventOrganizerId == updatedActivityRegist.Participant_ID)
            {
                return BadRequest("The event organizer cannot participate in the activities of his own event!");
            }
                
            // Verifica se o Participante está inscrito no evento
            var isRegistedOnEvent = await _context.Set<activity_info>()
                .Where(e => e.id == updatedActivityRegist.Activity_ID)
                .SelectMany(e => e._event.event_regists)
                .FirstOrDefaultAsync(er => er.participant_id == updatedActivityRegist.Participant_ID);

            if (isRegistedOnEvent == null)
            {
                return BadRequest("The user hasn't a registration for the event where this activity belong!");
            }

            // Verifica se o Participante já está inscrito na atividade
            int participantRegist = await _context.Set<activity_participant>()
                .CountAsync(regist => regist.activity_id == updatedActivityRegist.Activity_ID
                                      && regist.participant_id == updatedActivityRegist.Participant_ID);

            if (participantRegist > 0)
            {
                return BadRequest("The user already has a registration for this activity!");
            }

            await _activityParticipantRepository.UpdateActivityParticipant(updatedActivityRegist);

            return NoContent();
        }
        
        
        // DELETE: api/ActivityParticipant/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteActivityParticipant(Guid id)
        {
            int activityRegistrationDeletedId = await _activityParticipantRepository.DeleteActivityParticipant(id);

            if (activityRegistrationDeletedId == 200)
            {
                return Ok();
            }
            
            return NoContent();
        }
    }
}
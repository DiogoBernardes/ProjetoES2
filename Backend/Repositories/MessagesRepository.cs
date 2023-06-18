using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Messages;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class MessagesRepository : IMessageRepository
{
     private readonly ES2DbContext _context;

        public MessagesRepository(ES2DbContext context)
        {
            _context = context;
        }

        public async Task<List<MessagesModel>> GetOrganizerMessages(Guid organizerId)
        {
            return await _context.Set<message>()
                .Where(message => message.organizer_id == organizerId)
                .Select(message => new MessagesModel()
                {
                    ID = message.id,
                    Event_Name = message._event.name,
                    Organizer_Name = message.organizer.name,
                    Participant_Name = message.participant.name,
                    Message_Content = message.message_content
                })
                .ToListAsync();
        }

        public async Task<List<MessagesModel>> GetParticipantMessages(Guid participantId)
        {
            return await _context.Set<message>()
                .Where(message => message.participant_id == participantId)
                .Select(message => new MessagesModel()
                {
                    ID = message.id,
                    Event_Name = message._event.name,
                    Organizer_Name = message.organizer.name,
                    Participant_Name = message.participant.name,
                    Message_Content = message.message_content
                })
                .ToListAsync();
        }
        
        
        public async Task<MessagesModel> CreateMessage(MessagesModel message) {
            _context.Set<message>().Add(new message() {
                organizer_id = message.Organizer_ID,
                participant_id = message.Participant_ID,
                event_id = message.Event_ID,
                message_content = message.Message_Content,
            });
            await _context.SaveChangesAsync();
            return message;
        }
        
}
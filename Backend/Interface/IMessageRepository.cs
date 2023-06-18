using BusinessLogic.Models.Messages;

namespace Backend.Interface;

public interface IMessageRepository
{
    Task<List<MessagesModel>> GetOrganizerMessages(Guid id);
    Task<List<MessagesModel>> GetParticipantMessages(Guid id);
    Task<MessagesModel> CreateMessage(MessagesModel newMessage);
    
   
}
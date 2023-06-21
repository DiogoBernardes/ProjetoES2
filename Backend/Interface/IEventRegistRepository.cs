using BusinessLogic.Models.Event;
using BusinessLogic.Models.Event.regist;

namespace Backend.Interface;

public interface IEventRegistRepository
{
    Task<List<EventRegistModel>> GetEventsRegist();
    Task<EventRegistModel> GetEventRegist(Guid id);
    Task<CreateEventRegistModal> CreateEventRegist(CreateEventRegistModal newEntity);
    
    Task UpdateEventRegist(EventRegistModel newEntity);
    Task<int> DeleteEventRegist(Guid id);
    Task<List<Guid>> GetEventRegistIdsByEvent(Guid eventId);
    Task<List<EventRegistModel>> GetAllRegistsOnEvent(Guid id);
    Task<List<EventRegistModel>> GetEventRegistByParticipant(Guid participantId);
    Task<List<EventRegistModel>> GetEventsByUser(Guid userId);
    Task CancelEventRegist(Guid id);
}
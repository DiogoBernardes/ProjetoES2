using BusinessLogic.Models.Event;

namespace Backend.Interface;

public interface IEventRegistRepository
{
    Task<List<EventRegistModel>> GetEventsRegist();
    Task<EventRegistModel> GetEventRegist(Guid id);
    Task<EventRegistModel> CreateEventRegist(EventRegistModel newEntity);
    
    Task UpdateEventRegist(EventRegistModel newEntity);
    Task<int> DeleteEventRegist(Guid id);
}
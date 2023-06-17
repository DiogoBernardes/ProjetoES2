using BusinessLogic.Models.Event;

namespace Backend.Interface;

public interface IEventRepository
{    
    Task<List<EventModel>> GetEvents();
    Task<EventModel> GetEvent(Guid id);
    Task<EventModel> CreateEvent(EventModel newEntity);
    
    Task UpdateEvent(EventModel newEntity);
    Task<int> DeleteEvent(Guid id);
}
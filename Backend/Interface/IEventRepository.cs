using BusinessLogic.Models.Event;

namespace Backend.Interface;

public interface IEventRepository
{    
    Task<List<EventModel>> GetEvents();
    Task<EventModel> GetEvent(Guid id);
    Task<CreateEventModel> CreateEvent(CreateEventModel newEntity);
    
    Task UpdateEvent(UpdateEventModel newEntity);
    Task<int> DeleteEvent(Guid id);

}
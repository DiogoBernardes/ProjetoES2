using BusinessLogic.Models.Event;

namespace Backend.Interface;

public interface IEventTicketRepository
{
    Task<List<EventTicketModel>> GetEventTickets();
    Task<EventTicketModel> GetEventTicket(Guid id);
    Task<EventTicketModel> CreateEventTicket(EventTicketModel newEventTicket);
    
    Task UpdateEventTicket(EventTicketModel newEventTicket);
    Task<int> DeleteEventTicket(Guid id);
}
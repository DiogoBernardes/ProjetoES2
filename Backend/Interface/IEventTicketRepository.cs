using BusinessLogic.Models.Event;
using BusinessLogic.Models.Event.ticket;

namespace Backend.Interface;

public interface IEventTicketRepository
{
    Task<List<EventTicketModel>> GetEventTickets();
    Task<EventTicketModel> GetEventTicket(Guid id);
    Task<EventTicketModel> CreateEventTicket(EventTicketModel newEventTicket);
    
    Task UpdateEventTicket(EventTicketModel newEventTicket);
    Task<int> DeleteEventTicket(Guid id);
    Task<List<EventTicketModel>> GetEventTicketsByEvent(Guid eventId);
}
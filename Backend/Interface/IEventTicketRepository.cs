using BusinessLogic.Entities;
using BusinessLogic.Models.Event;
using BusinessLogic.Models.Event.ticket;

namespace Backend.Interface;

public interface IEventTicketRepository
{
    Task<List<EventTicketModel>> GetEventTickets();
    Task<EventTicketModel> GetEventTicket(Guid id);
    Task<CreateEventTicketModel> CreateEventTicket(CreateEventTicketModel newEventTicket);
    
    Task UpdateEventTicket(EditEventTicketModel newEventTicket);
    Task<int> DeleteEventTicket(Guid id);
    Task<List<EventTicketModel>> GetEventTicketsByEvent(Guid eventId);
    Task<List<TicketTypeModel>> GetAvailableTicketTypes();
    Task<event_ticket?> GetEventTicketByType(Guid eventId, Guid ticketTypeId);
}
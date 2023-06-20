using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Event.ticket;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class EventTicketRepository : IEventTicketRepository
{
    private readonly ES2DbContext _context;

    public EventTicketRepository(ES2DbContext context)
    {
        _context = context;
    }
    
    public async Task<List<EventTicketModel>> GetEventTickets() {
        return await _context.Set<event_ticket>().Select(eventTicket => new EventTicketModel() {
            ID = eventTicket.id,
            TicketType_ID = eventTicket.ticket_type,
            Event_ID = eventTicket.event_id,
            Event_name = eventTicket._event.name,
            Quantity = eventTicket.quantity,
            Price = eventTicket.price,
            TicketType =  new TicketTypeModel()
            {
                ID = eventTicket.ticket_typeNavigation.id,
                Name = eventTicket.ticket_typeNavigation.name
            },
        }).ToListAsync();
    }

    public async Task<EventTicketModel> GetEventTicket(Guid id)
    {
        return _context.Set<event_ticket>().Select(eventTicket => new EventTicketModel()
        {
            ID = eventTicket.id,
            TicketType_ID = eventTicket.ticket_type,
            Event_ID = eventTicket.event_id,
            Event_name = eventTicket._event.name,
            Quantity = eventTicket.quantity,
            Price = eventTicket.price,
            TicketType =  new TicketTypeModel()
            {
            ID = eventTicket.ticket_typeNavigation.id,
            Name = eventTicket.ticket_typeNavigation.name
        },
        }).FirstOrDefault(c => c.ID == id) ?? throw new InvalidOperationException("Event Ticket not found!");
    }
    public async Task<List<EventTicketModel>> GetEventTicketsByEvent(Guid eventId)
    {
        return await _context.Set<event_ticket>()
            .Where(eventTicket => eventTicket.event_id == eventId)
            .Select(eventTicket => new EventTicketModel()
            {
                ID = eventTicket.id,
                TicketType_ID = eventTicket.ticket_type,
                Event_ID = eventTicket.event_id,
                Event_name = eventTicket._event.name,
                Quantity = eventTicket.quantity,
                Price = eventTicket.price,
                TicketType = new TicketTypeModel()
                {
                    ID = eventTicket.ticket_typeNavigation.id,
                    Name = eventTicket.ticket_typeNavigation.name
                },
            }).ToListAsync();
    }

    public async Task<CreateEventTicketModel> CreateEventTicket(CreateEventTicketModel newEventTicket)
    {
        _context.Set<event_ticket>().Add(new event_ticket()
        {
            id = newEventTicket.ID,
            ticket_type = newEventTicket.ticket_ID,
            event_id = newEventTicket.event_ID,
            quantity = newEventTicket.quantity,
            price = newEventTicket.Price
        });

        await _context.SaveChangesAsync();

        return newEventTicket;
    }

    
    
    
    public async Task UpdateEventTicket(EditEventTicketModel newEventTicket)
    {
        var existingEventTicket = await _context.event_tickets.FirstOrDefaultAsync(c => c.id == newEventTicket.ID);

        if (existingEventTicket == null)
        {
            throw new ArgumentException("Event Ticket not found");
        }
        
        existingEventTicket.id = newEventTicket.ID;
        existingEventTicket.quantity = newEventTicket.Quantity;
        existingEventTicket.price = newEventTicket.Price;
        
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<int> DeleteEventTicket(Guid id)
    { 
        var eventTicketInfo = await _context.event_tickets.FirstOrDefaultAsync(c => c.id == id);

        if (eventTicketInfo == null)
        {
            throw new ArgumentException("Event Ticket not found");
        }

        _context.event_tickets.Remove(eventTicketInfo);
        await _context.SaveChangesAsync();

        return _context.SaveChangesAsync().Result;
    }
    
    public async Task<List<TicketTypeModel>> GetAvailableTicketTypes()
    {
           return await _context.Set<ticket_type>().Select(eventTicket => new TicketTypeModel() {
            ID = eventTicket.id,
            Name = eventTicket.name
        }).ToListAsync();
    }
    
    public async Task<event_ticket?> GetEventTicketByType(Guid eventId, Guid ticketTypeId)
    {
        return await _context.event_tickets
            .FirstOrDefaultAsync(t => t.event_id == eventId && t.ticket_type == ticketTypeId);
    }
    

}
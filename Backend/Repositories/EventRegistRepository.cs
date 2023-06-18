using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Event;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class EventRegistRepository : IEventRegistRepository
{
    
    private readonly ES2DbContext _context;

    public EventRegistRepository(ES2DbContext context)
    {
        _context = context;
    }
    
    public async Task<List<EventRegistModel>> GetEventsRegist() {
        return await _context.Set<event_regist>().Select(eventRegist => new EventRegistModel() {
            ID = eventRegist.id,
            Event_ID = eventRegist.event_id,
            Event_Name = eventRegist._event.name,
            Participant_ID = eventRegist.participant_id,
            Participant_Name = eventRegist.participant.name,
            State_ID = eventRegist.state_id,
            State_Name = eventRegist.state.state,
            Ticket_Type_ID = eventRegist.ticket_type_id,
            Ticket_Type_Name = eventRegist.ticket_type.name,
            Regist_Date = eventRegist.regist_date
        }).ToListAsync();
    }

    public async Task<EventRegistModel> GetEventRegist(Guid id)
    {
        return _context.Set<event_regist>().Select(eventRegist => new EventRegistModel()
        {
            ID = eventRegist.id,
            Event_ID = eventRegist.event_id,
            Event_Name = eventRegist._event.name,
            Participant_ID = eventRegist.participant_id,
            Participant_Name = eventRegist.participant.name,
            State_ID = eventRegist.state_id,
            State_Name = eventRegist.state.state,
            Ticket_Type_ID = eventRegist.ticket_type_id,
            Ticket_Type_Name = eventRegist.ticket_type.name,
            Regist_Date = eventRegist.regist_date
        }).FirstOrDefault(e => e.ID == id) ?? throw new InvalidOperationException("Registration not found!");
    }
    
    public async Task<EventRegistModel> CreateEventRegist(EventRegistModel newRegistration) {
        _context.Set<event_regist>().Add(new event_regist() {
            id = newRegistration.ID,
            event_id = newRegistration.Event_ID,
            participant_id = newRegistration.Participant_ID,
            state_id = newRegistration.State_ID,
            ticket_type_id = newRegistration.Ticket_Type_ID,
            regist_date = newRegistration.Regist_Date
        });
        await _context.SaveChangesAsync();
        return newRegistration;
    }
    
    public async Task UpdateEventRegist(EventRegistModel newRegistration)
    {
        var existingRegistration = await _context.event_regists.FirstOrDefaultAsync(u => u.id == newRegistration.ID);

        if (existingRegistration == null)
        {
            throw new ArgumentException("Registration not found");
        }

        existingRegistration.event_id = newRegistration.Event_ID;
        existingRegistration.participant_id = newRegistration.Participant_ID;
        existingRegistration.state_id = newRegistration.State_ID;
        existingRegistration.ticket_type_id = newRegistration.Ticket_Type_ID;
        existingRegistration.regist_date = newRegistration.Regist_Date;
            
        await _context.SaveChangesAsync();
    }
    
    public async Task<int> DeleteEventRegist(Guid id)
    { 
        var registInfo = await _context.event_regists.FirstOrDefaultAsync(u => u.id == id);

         if (registInfo == null)
         {
             throw new ArgumentException("Registration not found");
         }

         _context.event_regists.Remove(registInfo);
         await _context.SaveChangesAsync();

         return _context.SaveChangesAsync().Result;
    }
}
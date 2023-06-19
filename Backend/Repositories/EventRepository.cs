using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Event;
using Microsoft.EntityFrameworkCore;


namespace Backend.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ES2DbContext _context;

    public EventRepository(ES2DbContext context)
    {
        _context = context;
    }
    
    public async Task<List<EventModel>> GetEvents() {
        return await _context.Set<event_info>().Select(eventInfo => new EventModel() {
            ID = eventInfo.id,
            Organizer_Name = eventInfo.organizer.name,
            Name = eventInfo.name,
            Date_Hour = eventInfo.date_hour,
            Localization = eventInfo.localization,
            Description = eventInfo.description,
            Capacity = eventInfo.capacity,
            Category = new EventCategoryModel()
            {
                ID = eventInfo.categoryNavigation.id,
                Name = eventInfo.categoryNavigation.name
            }
        }).ToListAsync();
    }

    public async Task<EventModel> GetEvent(Guid id)
    {
        return _context.Set<event_info>().Select(eventInfo => new EventModel()
        {
            ID = eventInfo.id,
            Organizer_Name = eventInfo.organizer.name,
            Name = eventInfo.name,
            Date_Hour = eventInfo.date_hour,
            Localization = eventInfo.localization,
            Description = eventInfo.description,
            Capacity = eventInfo.capacity,
            Category = new EventCategoryModel()
            {
                ID = eventInfo.categoryNavigation.id,
                Name = eventInfo.categoryNavigation.name
            }
        }).FirstOrDefault(e => e.ID == id) ?? throw new InvalidOperationException("Event not found!");
    }
    public async Task<CreateEventModel> CreateEvent(CreateEventModel newEvent) {
    
        _context.Set<event_info>().Add(new event_info() {
            organizer_id = newEvent.Organizer_ID,
            name = newEvent.Name,
            date_hour = newEvent.Date_Hour,
            localization = newEvent.Localization,
            description = newEvent.Description,
            capacity = newEvent.Capacity,
            category = newEvent.Category_ID
        });
        await _context.SaveChangesAsync();
        return newEvent;
    }
    
    public async Task UpdateEvent(EventModel newEvent)
    {
        var existingEvent = await _context.event_infos.FirstOrDefaultAsync(u => u.id == newEvent.ID);

        if (existingEvent == null)
        {
            throw new ArgumentException("Event not found");
        }

        existingEvent.name = newEvent.Name;
        existingEvent.date_hour = newEvent.Date_Hour;
        existingEvent.localization = newEvent.Localization;
        existingEvent.description = newEvent.Description;
        existingEvent.capacity = newEvent.Capacity;
        existingEvent.category = newEvent.Category_ID;

        await _context.SaveChangesAsync();
    }
    public async Task<int> DeleteEvent(Guid id)
    { 
        var eventInfo = await _context.event_infos.FirstOrDefaultAsync(u => u.id == id);

         if (eventInfo == null)
         {
             throw new ArgumentException("Event not found");
         }

         _context.event_infos.Remove(eventInfo);
         await _context.SaveChangesAsync();

         return _context.SaveChangesAsync().Result;
    }
    
  
}
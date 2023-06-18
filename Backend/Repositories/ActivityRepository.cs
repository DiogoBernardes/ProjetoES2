using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Activity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly ES2DbContext _context;

    public ActivityRepository(ES2DbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ActivityModel>> GetActivities() {
        return await _context.Set<activity_info>().Select(activityInfo => new ActivityModel() {
            ID = activityInfo.id,
            Name = activityInfo.name,
            Description = activityInfo.description,
            Event_ID = activityInfo.event_id,
            Event_Name = activityInfo._event.name
        }).ToListAsync();
    }

    public async Task<ActivityModel> GetActivity(Guid id)
    {
        return _context.Set<activity_info>().Select(activityInfo => new ActivityModel()
        {
            ID = activityInfo.id,
            Name = activityInfo.name,
            Description = activityInfo.description,
            Event_ID = activityInfo.event_id,
            Event_Name = activityInfo._event.name
        }).FirstOrDefault(e => e.ID == id) ?? throw new InvalidOperationException("Activity not found!");
    }
    
    public async Task<ActivityModel> CreateActivity(ActivityModel newActivity) {
        _context.Set<activity_info>().Add(new activity_info() {
            name = newActivity.Name,
            description = newActivity.Description,
            event_id = newActivity.Event_ID
        });
        await _context.SaveChangesAsync();
        return newActivity;
    }
    
    public async Task UpdateActivity(ActivityModel newActivity)
    {
        var existingActivity = await _context.activity_infos.FirstOrDefaultAsync(u => u.id == newActivity.ID);

        if (existingActivity == null)
        {
            throw new ArgumentException("Activity not found");
        }
        
        existingActivity.name = newActivity.Name;
        existingActivity.description = newActivity.Description;
        existingActivity.event_id = newActivity.Event_ID;

        await _context.SaveChangesAsync();
    }
    
    public async Task<int> DeleteActivity(Guid id)
    { 
        var activityInfo = await _context.activity_infos.FirstOrDefaultAsync(u => u.id == id);

         if (activityInfo == null)
         {
             throw new ArgumentException("Activity not found");
         }

         _context.activity_infos.Remove(activityInfo);
         await _context.SaveChangesAsync();

         return _context.SaveChangesAsync().Result;
    }
}
using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Activity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ActivityParticipantRepository : IActivityParticipantRepository
{
    
 private readonly ES2DbContext _context;

    public ActivityParticipantRepository(ES2DbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ActivityParticipantModel>> GetActivitiesParticipants() {
        return await _context.Set<activity_participant>().Select(activitysParticipantsInfo => new ActivityParticipantModel() {
            ID = activitysParticipantsInfo.id,
            Activity_ID = activitysParticipantsInfo.activity_id,
            Activity_Name = activitysParticipantsInfo.activity.name,
            Participant_ID = activitysParticipantsInfo.participant_id,
            Participant_Name = activitysParticipantsInfo.participant.name
        }).ToListAsync();
    }

    public async Task<ActivityParticipantModel> GetActivityParticipant(Guid id)
    {
        return _context.Set<activity_participant>().Select(activityParticipantInfo => new ActivityParticipantModel()
        {
            ID = activityParticipantInfo.id,
            Activity_ID = activityParticipantInfo.activity_id,
            Activity_Name = activityParticipantInfo.activity.name,
            Participant_ID = activityParticipantInfo.participant_id,
            Participant_Name = activityParticipantInfo.participant.name
        }).FirstOrDefault(e => e.ID == id) ?? throw new InvalidOperationException("Activity Registration not found!");
    }
    
    public async Task<ActivityParticipantModel> CreateActivityParticipant(ActivityParticipantModel newActivityRegist) {
        _context.Set<activity_participant>().Add(new activity_participant() {
            participant_id = newActivityRegist.Participant_ID,
            activity_id = newActivityRegist.Activity_ID
        });
        await _context.SaveChangesAsync();
        return newActivityRegist;
    }
    
    public async Task UpdateActivityParticipant(ActivityParticipantModel newActivityRegist)
    {
        var existingActivityRegist = await _context.activity_participants.FirstOrDefaultAsync(u => u.id == newActivityRegist.ID);

        if (existingActivityRegist == null)
        {
            throw new ArgumentException("Activity Registration not found");
        }
        
        existingActivityRegist.participant_id = newActivityRegist.Participant_ID;
        existingActivityRegist.activity_id = newActivityRegist.Activity_ID;

        await _context.SaveChangesAsync();
    }
    
    public async Task<int> DeleteActivityParticipant(Guid id)
    { 
        var activityRegistrationInfo = await _context.activity_participants.FirstOrDefaultAsync(u => u.id == id);

         if (activityRegistrationInfo == null)
         {
             throw new ArgumentException("Activity Registration not found");
         }

         _context.activity_participants.Remove(activityRegistrationInfo);
         await _context.SaveChangesAsync();

         return _context.SaveChangesAsync().Result;
    }
}
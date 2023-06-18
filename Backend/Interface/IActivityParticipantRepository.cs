using BusinessLogic.Models.Activity;

namespace Backend.Interface;

public interface IActivityParticipantRepository
{
    Task<List<ActivityParticipantModel>> GetActivitiesParticipants();
    Task<ActivityParticipantModel> GetActivityParticipant(Guid id);
    Task<ActivityParticipantModel> CreateActivityParticipant(ActivityParticipantModel newEntity);
    Task UpdateActivityParticipant(ActivityParticipantModel newEntity);
    Task<int> DeleteActivityParticipant(Guid id);
}
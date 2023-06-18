using BusinessLogic.Models.Activity;

namespace Backend.Interface;

public interface IActivityRepository
{ 
    Task<List<ActivityModel>> GetActivities();
    Task<ActivityModel> GetActivity(Guid id);
    Task<ActivityModel> CreateActivity(ActivityModel newEntity);
    Task UpdateActivity(ActivityModel newEntity);
    Task<int> DeleteActivity(Guid id);
}
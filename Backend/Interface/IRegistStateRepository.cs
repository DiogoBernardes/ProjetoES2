using BusinessLogic.Models.Event;

namespace Backend.Interface;

public interface IRegistStateRepository
{
    Task<List<StateEventModel>> GetEventStates();
    Task<StateEventModel> GetEventState(Guid id);
    Task<Guid> GetStateIdByName(string stateName);
}
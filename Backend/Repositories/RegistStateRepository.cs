using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Event;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class RegistStateRepository: IRegistStateRepository
{
    private readonly ES2DbContext _context;

    public RegistStateRepository(ES2DbContext context)
    {
        _context = context;
    }
    public async Task<List<StateEventModel>> GetEventStates() {
        return await _context.Set<regist_state>().Select(stateEvent => new StateEventModel() {
            ID = stateEvent.id,
            Name= stateEvent.state
           
        }).ToListAsync();
    }

    public async Task<StateEventModel> GetEventState(Guid id)
    {
        return _context.Set<regist_state>().Select(stateEvent => new StateEventModel()
        {
            ID = stateEvent.id,
            Name= stateEvent.state
        }).FirstOrDefault(c => c.ID == id) ?? throw new InvalidOperationException("Event state not found!");
    }
    
    public async Task<Guid> GetStateIdByName(string stateName)
    {
        var states = await _context.Set<regist_state>().ToListAsync();

        var state = states.FirstOrDefault(s => s.state.Equals(stateName, StringComparison.OrdinalIgnoreCase));

        if (state == null)
        {
            throw new InvalidOperationException("State not found");
        }

        return state.id;
    }
}
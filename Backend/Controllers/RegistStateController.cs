using Backend.Interface;
using BusinessLogic.Entities;
using BusinessLogic.Models.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistStateController : ControllerBase
{
    
    private readonly IRegistStateRepository _registStateRepository;
   
        
    public RegistStateController(IRegistStateRepository registStateRepository)
    {
        _registStateRepository = registStateRepository;
    }
    
    // GET: api/EventTicket
    [HttpGet]
    [Authorize(Roles = "Admin,UserManager,User")]
    public async Task<IActionResult> GetEventStates()
    {
        var eventState = await _registStateRepository.GetEventStates();
        return Ok(eventState);

    }

    // GET: api/EventTicket
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,UserManager,User")]
    public async Task<IActionResult> GetEventTicket(Guid id)
    {
        var eventState = await _registStateRepository.GetEventState(id);

        if (eventState == null)
        {
            return NotFound();
        }

        return Ok(eventState);
    }
    
    // GET: api/RegistState/GetStateIdByName/{stateName}
    [HttpGet("GetStateIdByName/{stateName}")]
    [Authorize(Roles = "Admin,UserManager, User")]
    public async Task<IActionResult> GetStateIdByName(string stateName)
    {
        try
        {
            var stateId = await _registStateRepository.GetStateIdByName(stateName);
            return Ok(stateId);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
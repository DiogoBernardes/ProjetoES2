using Backend.Interface;
using BusinessLogic.Models.Activity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        // GET: api/Activity
        [HttpGet]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetActivities()
        {
            var getActivities = await _activityRepository.GetActivities();
            return Ok(getActivities);
         
        }

        // GET: api/Activity
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, UserManager")]
        public async Task<IActionResult> GetActivity(Guid id)
        {
            var getActivity = await _activityRepository.GetActivity(id);

            if (getActivity == null)
            {
                return NotFound();
            }

            return Ok(getActivity);
        }

        // POST: api/Activity
        [HttpPost]
        [Authorize(Roles = "Admin, UserManager,Users")]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityModel newActivity)
        {
            if (ModelState.IsValid)
            {
                var createdActivity = await _activityRepository.CreateActivity(newActivity);
                return Ok(createdActivity);
            }

            return BadRequest("Something went wrong!!");
        }
        
        // PUT: api/Activity/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateActivity(Guid id, [FromBody] ActivityModel updatedActivity)
        { 
            if (id != updatedActivity.ID)
            {
                return BadRequest("User Not Found!");
            }

            await _activityRepository.UpdateActivity(updatedActivity);

            return NoContent();
        }
        
        
        // DELETE: api/Activity/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            int activityDeletedId = await _activityRepository.DeleteActivity(id);

            if (activityDeletedId == 200)
            {
                return Ok();
            }
            
            return NoContent();
        }
        
    }
}
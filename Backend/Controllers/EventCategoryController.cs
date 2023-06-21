using Backend.Interface;
using BusinessLogic.Models.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventCategoryController : ControllerBase
{
    private readonly IEventCategoryRepository _eventCategoryRepository;

        public EventCategoryController(IEventCategoryRepository eventCategoryRepository)
        {
            _eventCategoryRepository = eventCategoryRepository;
        }

        // GET: api/Category
        [HttpGet("GetCategories")]
        [Authorize(Roles = "Admin,UserManager,User")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _eventCategoryRepository.GetCategories();
            return Ok(categories);
         
        }

        // GET: api/Category
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,UserManager")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _eventCategoryRepository.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        [Authorize(Roles = "Admin,UserManager,Users")]
        public async Task<IActionResult> CreateCategory([FromBody] EventCategoryModel newCategory)
        {
            if (ModelState.IsValid)
            {
                var createdCategory = await _eventCategoryRepository.CreateCategory(newCategory);
                return Ok(createdCategory);
            }

            return BadRequest("Something went wrong!!");
        }
        
        // PUT: api/Category/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] EventCategoryModel updatedCategory)
        { 
            if (id != updatedCategory.ID)
            {
                return BadRequest("Category Not Found!");
            }

            await _eventCategoryRepository.UpdateCategory(updatedCategory);

            return NoContent();
        }
        
        
        // DELETE: api/Category/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            int categoryDeletedId = await _eventCategoryRepository.DeleteCategory(id);

            if (categoryDeletedId == 200)
            {
                return Ok();
            }
            
            return NoContent();
        }

}

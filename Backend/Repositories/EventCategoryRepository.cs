using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models.Event;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class EventCategoryRepository : IEventCategoryRepository
{
    
    private readonly ES2DbContext _context;

    public EventCategoryRepository(ES2DbContext context)
    {
        _context = context;
    }
    
    public async Task<List<EventCategoryModel>> GetCategories() {
        return await _context.Set<event_category>().Select(eventCategory => new EventCategoryModel() {
            ID = eventCategory.id,
            Name = eventCategory.name
        }).ToListAsync();
    }

    public async Task<EventCategoryModel> GetCategory(Guid id)
    {
        return _context.Set<event_category>().Select(eventCategory => new EventCategoryModel()
        {
            ID = eventCategory.id,
            Name = eventCategory.name
        }).FirstOrDefault(c => c.ID == id) ?? throw new InvalidOperationException("Category not found!");
    }
    
    
    public async Task<EventCategoryModel> CreateCategory(EventCategoryModel newCategory) {
        _context.Set<event_category>().Add(new event_category() {
            name = newCategory.Name,
        });
        await _context.SaveChangesAsync();
        return newCategory;
    }
    
    public async Task UpdateCategory(EventCategoryModel newCategory)
    {
        var existingCategory = await _context.event_categories.FirstOrDefaultAsync(c => c.id == newCategory.ID);

        if (existingCategory == null)
        {
            throw new ArgumentException("Category not found");
        }
        existingCategory.name = newCategory.Name;

        await _context.SaveChangesAsync();
    }
    
    
    public async Task<int> DeleteCategory(Guid id)
    { 
        var categoryInfo = await _context.event_categories.FirstOrDefaultAsync(c => c.id == id);

         if (categoryInfo == null)
         {
             throw new ArgumentException("User not found");
         }

         _context.event_categories.Remove(categoryInfo);
         await _context.SaveChangesAsync();

         return _context.SaveChangesAsync().Result;
    }
    
}
using BusinessLogic.Models.Event;

namespace Backend.Interface;

public interface IEventCategoryRepository
{
    Task<List<EventCategoryModel>> GetCategories();
    Task<EventCategoryModel> GetCategory(Guid id);
    Task<EventCategoryModel> CreateCategory(EventCategoryModel newCategory);
    
    Task UpdateCategory(EventCategoryModel newCategory);
    Task<int> DeleteCategory(Guid id);
}
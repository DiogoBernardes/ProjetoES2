using BusinessLogic.Models.Event;

namespace Backend.Interface;

public interface IEventCategoryRepository
{
    Task<List<EventCategoryModel>> GetCategories();
    Task<EventCategoryModel> GetCategory(Guid id);
    Task<CreateCategoryModel> CreateCategory(CreateCategoryModel newCategory);
    
    Task UpdateCategory(EventCategoryModel newCategory);
    Task<int> DeleteCategory(Guid id);
}
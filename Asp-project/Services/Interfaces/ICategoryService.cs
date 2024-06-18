using Asp_project.Areas.Admin.ViewModels.VMCategory;
using Asp_project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp_project.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<SelectList> GetAllSelectAsync();
        
        Task<List<CategoryVM>> GetAllOrderByDescendingAsync();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(CategoryCreateVM category);
        Task<Category> GetWithProductAsync(int id);
        Task<Category> GetByIdAsync(int id);
        Task DeleteAsync(Category category);
        Task EditAsync(Category category, CategoryEditVM categoryEdit);
       
    }
}

using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
    }
}

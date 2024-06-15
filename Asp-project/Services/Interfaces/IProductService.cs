using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllWithProductImage();
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetProductsByCategoryIdAsync( int id);
    }
}

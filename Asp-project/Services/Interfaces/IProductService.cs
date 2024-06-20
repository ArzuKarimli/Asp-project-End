using Asp_project.Areas.Admin.ViewModels.Product;
using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllWithProductImage();
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetProductsByCategoryIdAsync( int id);
        Task CreateAsync(Product product);
        Task DeleteAsync(Product product);
        Task EditAsync(Product product,ProductEditVM editProduct);
        Task<List<Product>> SearchProductAsync(string searchText);
        Task<List<Product>> GetAllPaginationAsync(int page, int take = 4);
       
        Task<int> GetCountAsync();
    }
}

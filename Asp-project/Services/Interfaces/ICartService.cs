using Asp_project.ViewModel;

namespace Asp_project.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartVM>> GetCartProductsAsync();
        Task DeleteAsync(int id);
        Task IncrementProductCountAsync(int id);
        Task ReductionCounterAsync(int id);
        Task<(int count, decimal total)> AddProductToBasketAsync(int id);
    }
}

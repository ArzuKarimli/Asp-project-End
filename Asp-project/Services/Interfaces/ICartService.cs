using Asp_project.Models;
using Asp_project.ViewModel;

namespace Asp_project.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<BasketVM>> AddProductToBasketAsync(int id);
    }
}

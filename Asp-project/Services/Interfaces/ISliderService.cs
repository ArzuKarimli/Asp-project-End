using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
        Task<SliderInfo> GetSliderInfoAsync();
        Task<Slider> GetByIdForSliderAsync(int id);
        Task<SliderInfo> GetByIdForSliderInfoAsync(int id);
        Task AddAsync(Slider slider);
        Task DeleteSliderAsync(Slider slider);
        Task UpdateSliderImageAsync(Slider slider, IFormFile newImage);
    }
}

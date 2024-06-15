using Asp_project.Areas.Admin.ViewModels.SliderInfo;
using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<List<SliderInfo>> GetAll();
        Task<SliderInfo> GetByIdAsync(int id);
        Task<bool> ExistAsync(string title,string desc);
        Task CreateInfoAsync(SliderInfoCreateVM sliderInfo);
        Task DeleteInfoAsync(SliderInfo info);
        Task EditAsync(SliderInfo existInfo,SliderInfoEditVM info);
    }
}

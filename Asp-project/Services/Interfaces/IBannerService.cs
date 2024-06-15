using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface IBannerService
    {
        Task<List<Banner>> GetAllAsync();
    }
}

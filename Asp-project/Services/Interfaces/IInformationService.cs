using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface IInformationService
    {
        Task<List<Information>> GetAllFeatures();
        Task<List<Information>> GetAllCounter();
        Task<List<Information>> GetAllAsync();

    }
}

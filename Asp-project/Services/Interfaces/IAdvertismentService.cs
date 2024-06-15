using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface IAdvertismentService
    {
        Task<List<Advertisment>> GetAllAsync();
    }
}

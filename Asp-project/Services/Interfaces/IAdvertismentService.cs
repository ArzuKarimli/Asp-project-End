using Asp_project.Areas.Admin.ViewModels.Advertisment;
using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface IAdvertismentService
    {
        Task<List<Advertisment>> GetAllAsync();
        Task SetImageMain(int id);
        Task<Advertisment> GetByIdAsync(int id);
        Task DeleteAsync(Advertisment adv);
        Task CreateAsync(Advertisment adv);
        Task EditAsync(Advertisment adv);
    }
}

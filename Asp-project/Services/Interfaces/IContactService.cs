using Asp_project.ViewModel;

namespace Asp_project.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateMessage(ContactVM message);
    }
}

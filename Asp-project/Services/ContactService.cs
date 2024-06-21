using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;

namespace Asp_project.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        public ContactService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateMessage(ContactVM message)
        {
            await _context.Contacts.AddAsync(new Contact { Email = message.Email, Name = message.Name, Message = message.Message });
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace Asp_project.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}

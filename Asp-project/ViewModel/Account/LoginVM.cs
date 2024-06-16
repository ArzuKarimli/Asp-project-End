using System.ComponentModel.DataAnnotations;

namespace Asp_project.ViewModel.Account
{
    public class LoginVM
    {
            [Required]
            public string EmailOrUserName { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        
    }
}

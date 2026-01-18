using System.ComponentModel.DataAnnotations;

namespace EdukateMVC.ViewModels.UserViewModels
{
    public class LoginVM
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(255), MinLength(8), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool IsRemember { get; set; }
    }
}

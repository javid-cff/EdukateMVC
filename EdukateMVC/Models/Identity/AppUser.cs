using Microsoft.AspNetCore.Identity;

namespace EdukateMVC.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
    }
}

using EdukateMVC.Models;
using EdukateMVC.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EdukateMVC.Contexts
{
    public class EdukateDbContext : IdentityDbContext<AppUser>
    {
        public EdukateDbContext(DbContextOptions<EdukateDbContext> options) : base(options)
        {
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}

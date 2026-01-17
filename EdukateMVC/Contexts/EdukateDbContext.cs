using EdukateMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EdukateMVC.Contexts
{
    public class EdukateDbContext : DbContext
    {
        public EdukateDbContext(DbContextOptions<EdukateDbContext> options) : base(options)
        {
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}

using System.Threading.Tasks;
using EdukateMVC.Contexts;
using EdukateMVC.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EdukateMVC.Controllers
{
    [Authorize]
    public class CourseController(EdukateDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Select(x => new CourseGetVM
            {
                Id = x.Id,
                Title = x.Title,
                Rating = x.Rating,
                Review = x.Review,
                ImagePath = x.ImagePath,
                TeacherName = x.Teacher.FullName
            }).ToListAsync();

            return View(courses);
        }
    }
}

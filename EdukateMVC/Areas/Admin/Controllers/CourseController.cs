using System.Threading.Tasks;
using EdukateMVC.Contexts;
using EdukateMVC.Helpers;
using EdukateMVC.Models;
using EdukateMVC.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EdukateMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class CourseController : Controller
    {
        private readonly EdukateDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _folderPath;

        public CourseController(EdukateDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Select(x => new CourseGetVM()
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await _SendTeachersWithViewBag();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateVM vm)
        {
            await _SendTeachersWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            var isExistTeacher = await _context.Teachers.AnyAsync(x => x.Id == vm.TeacherId);

            if (!isExistTeacher)
            {
                ModelState.AddModelError("TeacherId", "This teacher is not found!");
                return View(vm);
            }

            if (!vm.Image.ChecSize(2))
            {
                ModelState.AddModelError("Image", "Image size must be maximum 2MB!");
                return View(vm);
            }

            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "Image format is invalid!");
                return View(vm);
            }

            string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);

            Course course = new Course()
            {
                Id = vm.Id,
                Title = vm.Title,
                Rating = vm.Rating,
                Review = vm.Review,
                ImagePath = uniqueFileName,
                TeacherId = vm.TeacherId,
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return BadRequest();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            string deletedImagePath = Path.Combine(_folderPath, course.ImagePath);

            FileHelper.FileDelete(deletedImagePath);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return BadRequest();

            CourseUpdateVM vm = new CourseUpdateVM()
            {
                Id = course.Id,
                Title = course.Title,
                Rating = course.Rating,
                Review = course.Review,
                TeacherId= course.TeacherId
            };

            await _SendTeachersWithViewBag();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateVM vm)
        {
            await _SendTeachersWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            var isExistTeacher = await _context.Teachers.AnyAsync(x => x.Id == vm.TeacherId);

            if (!isExistTeacher)
            {
                ModelState.AddModelError("TeacherId", "This teacher is not found!");
                return View(vm);
            }

            if (!vm.Image?.ChecSize(2) ?? false)
            {
                ModelState.AddModelError("Image", "Image size must be maximum 2MB!");
                return View(vm);
            }

            if (!vm.Image?.CheckType("image") ?? false)
            {
                ModelState.AddModelError("Image", "Image format is invalid!");
                return View(vm);
            }

            var existCourse = await _context.Courses.FindAsync(vm.Id);

            if (existCourse == null)
                return BadRequest();

            existCourse.Id = vm.Id;
            existCourse.Title = vm.Title;
            existCourse.Rating = vm.Rating;
            existCourse.Review = vm.Review;
            existCourse.TeacherId = vm.TeacherId;

            if (vm.Image is { })
            {
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);

                string deletedImagePath = Path.Combine(_folderPath, existCourse.ImagePath);
                FileHelper.FileDelete(deletedImagePath);

                existCourse.ImagePath = newImagePath;
            }

            _context.Courses.Update(existCourse);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task _SendTeachersWithViewBag()
        {
            var teachers = await _context.Teachers.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            }).ToListAsync();

            ViewBag.Teachers = teachers;
        }
    }
}

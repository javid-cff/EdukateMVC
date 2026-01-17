using System.Threading.Tasks;
using EdukateMVC.Contexts;
using EdukateMVC.Migrations;
using EdukateMVC.Models;
using EdukateMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EdukateMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class TeacherController(EdukateDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var teachers = await _context.Teachers.Select(x => new TeacherVM
            {
                Id = x.Id,
                FullName = x.FullName
            }).ToListAsync();

            return View(teachers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            Teacher teacher = new Teacher()
            {
                Id = vm.Id,
                FullName = vm.FullName,
            };

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
                return BadRequest();

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
                return BadRequest();

            TeacherVM vm = new TeacherVM()
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TeacherVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var existTeacher = await _context.Teachers.FindAsync(vm.Id);

            if (existTeacher == null)
                return BadRequest();

            existTeacher.Id = vm.Id;
            existTeacher.FullName = vm.FullName;

            _context.Teachers.Update(existTeacher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}

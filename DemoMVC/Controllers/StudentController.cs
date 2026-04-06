using Microsoft.AspNetCore.Mvc;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace DemoMVC.Controllers
{
    public class StudentController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        return View(student);
    }
        [HttpPost]
        public async Task<IActionResult> Edit(string StudentCode, Student student)
        {
            if (StudentCode != student.StudentCode)
            {
                return RedirectToAction("NotFoundPage");
            }

            if (ModelState.IsValid)
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

        return View(student);
    }
        public async Task<IActionResult> Delete(string studentCode)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == studentCode);

            if (student == null)
                return RedirectToAction("NotFoundPage");
            return View(student);
        }
        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string studentCode)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == studentCode);

            if (student == null)
                return RedirectToAction("NotFoundPage");

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
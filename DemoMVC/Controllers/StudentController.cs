using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using DemoMVC.Models.ViewModels;
using OfficeOpenXml;

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var result = await _context.Students
                            .Select(s => new StudentFaculty
                            {
                                StudentCode = s.StudentCode,
                                FullName = s.FullName,
                                FacultyName = s.Faculty!.FacultyName
                            })
                            .ToListAsync();
            return View(result);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentCode,FullName,FacultyId")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (StudentExists(student.StudentCode))
                {
                    ModelState.AddModelError("StudentCode", "Ma sinh vien da ton tai");
                    return View(student);
                }
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentCode,FullName,FacultyId")] Student student)
        {
            if (id != student.StudentCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return View(student);
        }

        // GET: Student/Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentCode == id);
        }
        public IActionResult Import()
        {
            return View();
        }
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Import(IFormFile file)
{
    if (file == null || file.Length == 0)
        return Content("File không hợp lệ");

    ExcelPackage.License.SetNonCommercialPersonal("Vuong");

    using var stream = new MemoryStream();
    await file.CopyToAsync(stream);

    using var package = new ExcelPackage(stream);
    var worksheet = package.Workbook.Worksheets[0];
    int rowCount = worksheet.Dimension.Rows;

    int success = 0;

    for (int row = 2; row <= rowCount; row++)
    {
        var code = worksheet.Cells[row, 1].Text.Trim();
        var name = worksheet.Cells[row, 2].Text.Trim();
        var facultyId = worksheet.Cells[row, 3].Text.Trim();

        if (string.IsNullOrEmpty(code)) continue;

        if (_context.Students.Any(s => s.StudentCode == code))
            continue;

        var faculty = await _context.Faculties.FindAsync(facultyId);
        if (faculty == null)
            continue;

        var student = new Student
        {
            StudentCode = code,
            FullName = name,
            FacultyId = facultyId
        };

        _context.Students.Add(student);
        success++;
    }
    await _context.SaveChangesAsync();
    TempData["Message"] = $"Import thành công {success} sinh viên!";
    return RedirectToAction(nameof(Index));
}
    }
}
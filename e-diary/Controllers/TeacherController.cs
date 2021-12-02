using e_diary.Data;
using e_diary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList.Mvc;
using PagedList;

namespace e_diary.Controllers
{
    public class TeacherController : Controller
    {
        public DBContext _TeacherContext;

        public TeacherController(DBContext context)
        {
            _TeacherContext = context;
        }
        
        public IActionResult Index(int? i, int? page, string searchText = "")
        {
            int pageCurrent = page ?? 1; //page == null ? 1 : page
            int pageMaxSize = 3;

            List<Teacher> teachers = _TeacherContext.Teachers.ToList();

            if (searchText != "" && searchText != null)
            {
                teachers = _TeacherContext.Teachers.Where(p => p.FName.Contains(searchText)).ToList();
            }
            else
                teachers = _TeacherContext.Teachers.ToList();

            return View(teachers.ToPagedList(pageCurrent, pageMaxSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _TeacherContext.Teachers.Add(teacher);
                await _TeacherContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            var TeacherInDb = await _TeacherContext.Teachers.FirstOrDefaultAsync(e => e.ID == id);

            if (TeacherInDb == null)
                return NotFound();

            return View(TeacherInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Teacher teacher)
        {
            if (!ModelState.IsValid)
                return View(teacher);

            _TeacherContext.Teachers.Update(teacher);

            await _TeacherContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();

            var teacherInDb = await _TeacherContext.Teachers.FirstOrDefaultAsync(e => e.ID == id);

            if (teacherInDb == null)
                return NotFound();

            _TeacherContext.Teachers.Remove(teacherInDb);

            await _TeacherContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

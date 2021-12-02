using e_diary.Data;
using e_diary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_diary.Controllers
{
    public class SubjectController : Controller
    {
        public DBContext _SubjectContext;

        public SubjectController(DBContext context)
        {
            _SubjectContext = context;
        }
        public async Task<IActionResult> Index(string searchText = "")
        {
            var subjects = await _SubjectContext.Subjects.ToListAsync();
            if (searchText != "" && searchText != null)
            {
                subjects = _SubjectContext.Subjects.Where(p => p.Name.Contains(searchText)).ToList();
            }
            else
                subjects = _SubjectContext.Subjects.ToList();

            return View(subjects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _SubjectContext.Subjects.Add(subject);
                await _SubjectContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            var SubjectInDb = await _SubjectContext.Subjects.FirstOrDefaultAsync(e => e.ID == id);

            if (SubjectInDb == null)
                return NotFound();

            return View(SubjectInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Subject subject)
        {
            if (!ModelState.IsValid)
                return View(subject);

            _SubjectContext.Subjects.Update(subject);

            await _SubjectContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();

            var subjectInDb = await _SubjectContext.Subjects.FirstOrDefaultAsync(e => e.ID == id);

            if (subjectInDb == null)
                return NotFound();

            _SubjectContext.Subjects.Remove(subjectInDb);

            await _SubjectContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

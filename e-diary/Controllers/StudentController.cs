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
    public class StudentController : Controller
    {
        public DBContext _StudentContext;

        public StudentController(DBContext context)
        {
            _StudentContext = context;
        }

        public IActionResult Index(int pg = 1, string searchText = "")
        {
            List<Student> students = _StudentContext.Students.ToList();

            if (searchText != "" && searchText != null)
            {
                students = _StudentContext.Students.Where(p => p.FName.Contains(searchText)).ToList();
            }
            else
                students = _StudentContext.Students.ToList();

            const int elementsPerPage = 3;
            if (pg < 1)
                pg = 1;

            int recsCount = students.Count();

            var pager = new Pager(recsCount, pg, elementsPerPage);

            int recSkip = (pg - 1) * elementsPerPage;

            var data = students.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            //return View(students);
            return View(data);
        }

        

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _StudentContext.Students.Add(student);
                await _StudentContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            var StudentInDb = await _StudentContext.Students.FirstOrDefaultAsync(e => e.ID == id);

            if (StudentInDb == null)
                return NotFound();

            return View(StudentInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _StudentContext.Students.Update(student);

            await _StudentContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();

            var studentInDb = await _StudentContext.Students.FirstOrDefaultAsync(e => e.ID == id);

            if (studentInDb == null)
                return NotFound();

            _StudentContext.Students.Remove(studentInDb);

            await _StudentContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

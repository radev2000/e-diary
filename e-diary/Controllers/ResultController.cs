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
    public class ResultController : Controller
    {
        public DBContext _ResultContext;

        public ResultController(DBContext context)
        {
            _ResultContext = context;
        }
        public async Task<IActionResult> Index()
        {
            var results = await _ResultContext.Results.ToListAsync();
            return View(results);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Result result)
        {
            if (ModelState.IsValid)
            {
                _ResultContext.Results.Add(result);
                await _ResultContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            var ResultInDb = await _ResultContext.Results.FirstOrDefaultAsync(e => e.ID == id);

            if (ResultInDb == null)
                return NotFound();

            return View(ResultInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Result result)
        {
            if (!ModelState.IsValid)
                return View(result);

            _ResultContext.Results.Update(result);

            await _ResultContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();

            var resultInDb = await _ResultContext.Results.FirstOrDefaultAsync(e => e.ID == id);

            if (resultInDb == null)
                return NotFound();

            _ResultContext.Results.Remove(resultInDb);

            await _ResultContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

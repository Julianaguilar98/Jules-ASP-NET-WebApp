using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JulesWebApp.Data;
using JulesWebApp.Models;

namespace JulesWebApp.Controllers
{
    public class JulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jules
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jules.ToListAsync());
        }

        // GET: Jules/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        // POST: Jokes/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.Jules.Where( j => j.JulesQuestion.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Jules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jules = await _context.Jules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jules == null)
            {
                return NotFound();
            }

            return View(jules);
        }

        // GET: Jules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JulesQuestion,JulesAnswer")] Jules jules)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jules);
        }

        // GET: Jules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jules = await _context.Jules.FindAsync(id);
            if (jules == null)
            {
                return NotFound();
            }
            return View(jules);
        }

        // POST: Jules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JulesQuestion,JulesAnswer")] Jules jules)
        {
            if (id != jules.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JulesExists(jules.Id))
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
            return View(jules);
        }

        // GET: Jules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jules = await _context.Jules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jules == null)
            {
                return NotFound();
            }

            return View(jules);
        }

        // POST: Jules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jules = await _context.Jules.FindAsync(id);
            _context.Jules.Remove(jules);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JulesExists(int id)
        {
            return _context.Jules.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HeartyBeat.Data;
using HeartyBeatApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HeartyBeat.Controllers
{
    [Authorize]
    public class TrackersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public TrackersController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Trackers
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Tracker != null ? 
                          View(await _context.Tracker.Where(item => item.UserId == userId).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tracker'  is null.");
        }

        // GET: Trackers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tracker == null)
            {
                return NotFound();
            }

            var tracker = await _context.Tracker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tracker == null)
            {
                return NotFound();
            }

            return View(tracker);
        }

        // GET: Trackers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trackers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HeartRate,Weight,Height,Steps,Id")] Tracker tracker)
        {
            if (ModelState.IsValid)
            {
                tracker.UserId= _userManager.GetUserId(User);
                _context.Add(tracker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tracker);
        }

        // GET: Trackers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tracker == null)
            {
                return NotFound();
            }

            var tracker = await _context.Tracker.FindAsync(id);
            if (tracker == null)
            {
                return NotFound();
            }
            return View(tracker);
        }

        // POST: Trackers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HeartRate,Weight,Height,Steps,Id")] Tracker tracker)
        {
            if (id != tracker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tracker.UserId = _userManager.GetUserId(User);
                    _context.Update(tracker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackerExists(tracker.Id))
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
            return View(tracker);
        }

        // GET: Trackers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tracker == null)
            {
                return NotFound();
            }

            var tracker = await _context.Tracker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tracker == null)
            {
                return NotFound();
            }

            return View(tracker);
        }

        // POST: Trackers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tracker == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tracker'  is null.");
            }
            var tracker = await _context.Tracker.FindAsync(id);
            if (tracker != null)
            {
                _context.Tracker.Remove(tracker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackerExists(int id)
        {
          return (_context.Tracker?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

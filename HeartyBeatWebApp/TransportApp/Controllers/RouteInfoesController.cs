using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransportApp.Data;

namespace TransportApp.Controllers
{
    public class RouteInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RouteInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RouteInfoes
        public async Task<IActionResult> Index()
        {
              return _context.Routes != null ? 
                          View(await _context.Routes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Routes'  is null.");
        }

        // GET: RouteInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Routes == null)
            {
                return NotFound();
            }

            var routeInfo = await _context.Routes
                .Include(m => m.Schedules)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routeInfo == null)
            {
                return NotFound();
            }

            return View(routeInfo);
        }

        // GET: RouteInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RouteInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Id")] RouteInfo routeInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routeInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(routeInfo);
        }

        // GET: RouteInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Routes == null)
            {
                return NotFound();
            }

            var routeInfo = await _context.Routes.FindAsync(id);
            if (routeInfo == null)
            {
                return NotFound();
            }
            return View(routeInfo);
        }

        // POST: RouteInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,Id")] RouteInfo routeInfo)
        {
            if (id != routeInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteInfoExists(routeInfo.Id))
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
            return View(routeInfo);
        }

        // GET: RouteInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Routes == null)
            {
                return NotFound();
            }

            var routeInfo = await _context.Routes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (routeInfo == null)
            {
                return NotFound();
            }

            return View(routeInfo);
        }

        // POST: RouteInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Routes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Routes'  is null.");
            }
            var routeInfo = await _context.Routes.FindAsync(id);
            if (routeInfo != null)
            {
                _context.Routes.Remove(routeInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteInfoExists(int id)
        {
          return (_context.Routes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

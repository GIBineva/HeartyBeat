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
    public class HealthyTIpsPersonalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HealthyTIpsPersonalsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HealthyTIpsPersonals
        public async Task<IActionResult> Index()
        {
              return _context.HealthyTIpsPersonal != null ? 
                          View(await _context.HealthyTIpsPersonal.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.HealthyTIpsPersonal'  is null.");
        }

        // GET: HealthyTIpsPersonals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HealthyTIpsPersonal == null)
            {
                return NotFound();
            }

            var healthyTIpsPersonal = await _context.HealthyTIpsPersonal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (healthyTIpsPersonal == null)
            {
                return NotFound();
            }

            return View(healthyTIpsPersonal);
        }

        // GET: HealthyTIpsPersonals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HealthyTIpsPersonals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("tipFromUser,UserId,Id")] HealthyTIpsPersonal healthyTIpsPersonal)
        {
            if (ModelState.IsValid)
            {
                healthyTIpsPersonal.UserId = _userManager.GetUserId(User);
                _context.Add(healthyTIpsPersonal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(healthyTIpsPersonal);
        }

        // GET: HealthyTIpsPersonals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HealthyTIpsPersonal == null)
            {
                return NotFound();
            }

            var healthyTIpsPersonal = await _context.HealthyTIpsPersonal.FindAsync(id);
            if (healthyTIpsPersonal == null)
            {
                return NotFound();
            }
            return View(healthyTIpsPersonal);
        }

        // POST: HealthyTIpsPersonals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("tipFromUser,UserId,Id")] HealthyTIpsPersonal healthyTIpsPersonal)
        {
            if (id != healthyTIpsPersonal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    healthyTIpsPersonal.UserId = _userManager.GetUserId(User);
                    _context.Update(healthyTIpsPersonal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthyTIpsPersonalExists(healthyTIpsPersonal.Id))
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
            return View(healthyTIpsPersonal);
        }

        // GET: HealthyTIpsPersonals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HealthyTIpsPersonal == null)
            {
                return NotFound();
            }

            var healthyTIpsPersonal = await _context.HealthyTIpsPersonal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (healthyTIpsPersonal == null)
            {
                return NotFound();
            }

            return View(healthyTIpsPersonal);
        }

        // POST: HealthyTIpsPersonals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HealthyTIpsPersonal == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HealthyTIpsPersonal'  is null.");
            }
            var healthyTIpsPersonal = await _context.HealthyTIpsPersonal.FindAsync(id);
            if (healthyTIpsPersonal != null)
            {
                _context.HealthyTIpsPersonal.Remove(healthyTIpsPersonal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HealthyTIpsPersonalExists(int id)
        {
          return (_context.HealthyTIpsPersonal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

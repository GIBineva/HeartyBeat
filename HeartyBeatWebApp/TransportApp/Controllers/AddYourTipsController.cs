using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HeartyBeat.Data;
using HeartyBeatApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NuGet.DependencyResolver;

namespace HeartyBeat.Controllers
{
    [Authorize]
    public class AddYourTipsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AddYourTipsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AddYourTips
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            return _context.HealthyTIpsPersonal != null ? 
                          View(await _context.HealthyTIpsPersonal.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.HealthyTIpsPersonal'  is null.");
        }

        // GET: AddYourTips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HealthyTIpsPersonal == null)
            {
                return NotFound();
            }

            var addYourTips = await _context.HealthyTIpsPersonal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addYourTips == null)
            {
                return NotFound();
            }

            return View(addYourTips);
        }

        // GET: AddYourTips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddYourTips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipFromUser,Username,Id")] AddYourTips addYourTips)
        {
            if (ModelState.IsValid)
            {
                addYourTips.UserId = _userManager.GetUserId(User);
                _context.Add(addYourTips);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addYourTips);
        }

        // GET: AddYourTips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HealthyTIpsPersonal == null)
            {
                return NotFound();
            }

            var addYourTips = await _context.HealthyTIpsPersonal.FindAsync(id);
            if (addYourTips == null)
            {
                return NotFound();
            }
            return View(addYourTips);
        }

        // POST: AddYourTips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipFromUser,Username,Id")] AddYourTips addYourTips)
        {
            if (id != addYourTips.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    addYourTips.UserId = _userManager.GetUserId(User);
                    _context.Update(addYourTips);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddYourTipsExists(addYourTips.Id))
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
            return View(addYourTips);
        }

        // GET: AddYourTips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HealthyTIpsPersonal == null)
            {
                return NotFound();
            }

            var addYourTips = await _context.HealthyTIpsPersonal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addYourTips == null)
            {
                return NotFound();
            }

            return View(addYourTips);
        }

        // POST: AddYourTips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HealthyTIpsPersonal == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HealthyTIpsPersonal'  is null.");
            }
            var addYourTips = await _context.HealthyTIpsPersonal.FindAsync(id);
            if (addYourTips != null)
            {
                _context.HealthyTIpsPersonal.Remove(addYourTips);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddYourTipsExists(int id)
        {
          return (_context.HealthyTIpsPersonal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

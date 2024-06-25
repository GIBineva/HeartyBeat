using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HeartyBeatApp.Models;
using HeartyBeatApp.Data;
using Microsoft.AspNetCore.Identity;
using HeartyBeat.Data;
using Microsoft.AspNetCore.Authorization;

namespace HeartyBeatApp.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CollectionController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var obtainedRewards = user.Obtained;
            var obtainedIds = obtainedRewards.Select(item => item.Id).ToList();
            var notObtainedRewards = _context.Reward.Where(item => !obtainedIds.Contains(item.Id));

            ViewBag.ObtainedRewards = obtainedRewards;
            ViewBag.NotObtainedRewards = notObtainedRewards;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRewardStatusAsync([FromBody] Reward reward)
        {
            var existingReward = _context.Reward.FirstOrDefault(r => r.Message == reward.Message);
            if (existingReward != null)
            {
                var user = await _userManager.GetUserAsync(User);
                user.Obtained.Add(existingReward);
                _context.SaveChanges();
                return Ok(); // Return Ok status if update is successful
            }

            return NotFound(); // Return NotFound if reward not found
        }
    }
}
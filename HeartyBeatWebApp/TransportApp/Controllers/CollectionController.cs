using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HeartyBeatApp.Models;

namespace HeartyBeatApp.Controllers
{
    public class CollectionController : Controller
    {
        private static List<Reward> _rewards = new List<Reward>
        {
                new Reward { Message = "Woahh you got .....", ImageUrl = "/images/HeartCat.jpg" },
                new Reward { Message = "GASP what a surprise!!", ImageUrl = "/images/HeartCat.jpg" },
                new Reward { Message = "You pulled: ", ImageUrl = "/images/HeartCat.jpg" },
                new Reward { Message = "Oh em gi.. you pulled..", ImageUrl = "/images/HeartCat.jpg" },
                new Reward { Message = "Look what you got!!", ImageUrl = "/images/HeartCat.jpg" },
        };

        public IActionResult Index()
        {
            var obtainedRewards = _rewards.Where(r => r.Obtained).ToList();
            var notObtainedRewards = _rewards.Where(r => !r.Obtained).ToList();

            ViewBag.ObtainedRewards = obtainedRewards;
            ViewBag.NotObtainedRewards = notObtainedRewards;

            return View();
        }

        [HttpPost]
        public IActionResult UpdateRewardStatus([FromBody] Reward reward)
        {
            var existingReward = _rewards.FirstOrDefault(r => r.Message == reward.Message);
            if (existingReward != null)
            {
                existingReward.Obtained = true;
                return Ok(); // Return Ok status if update is successful
            }

            return NotFound(); // Return NotFound if reward not found
        }
    }
}
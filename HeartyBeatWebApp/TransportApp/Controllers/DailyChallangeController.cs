using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HeartyBeatApp.Controllers
{
    public class DailyChallengesController : Controller
    {
        private readonly List<string> _challenges = new List<string>
        {
            "Include fiber into your diet - Beans and Fruits and Oatmeal and Popcorn are foods full of fiber <3",
            "Make time for breakfast - The first meal for the day than can lead to positivity trough the day!",
            "Eat fish - lowers blood pressure and help reduce the risk of a heart attack or stroke!!",
            "Eat nuts - good sources of protein healthy fats fibres vitamins and minerals!!",
            "Limit your salt intake - can cause high blood pressure!!",
            "Reduce your saturated fat intake - too much saturated fat can cause cholesterol to build up in your arteries.",
            "Drink tea - May improve blood pressure and has so much more benefits",
            "Eat dark chocolate - as tasty as milk chocolate but ttimes more healthy <3",
            "Move your body throughout the day - WREK IT GIRL!!",
            "Practice yoga - all you need is a yoga mat and an youtube video ",
            "Try strength training - helps with stress and you can benefit from the results!! ",
            "Try interval training - burns calories and reduces blood pressure",
            "Try dancing - fun way of working out!!",
            "Go for a walk - a nice way to clear your mind from stress",
            "Take the stairs - whoops the lift is suddently broken.. wonder who did that...",
            "Use housework as exercise - clean house and happy body <3",
            "Be a kid - pfft who said this was for kids and enjoy your life!! ",
            "Engage in hobbies - a nice way to spend some me time",
            "Laugh out loud - who cares if people hear you? There are 5 billion pple on earth",
            "Manage your stress - there are so many ways of that (breathing exercises and so much more)",
            "Take the scenic route home - explore your hometown or go trough your fave route!!"
        };

        private readonly List<(string Message, string ImageUrl)> _rewards = new List<(string, string)>
        {
            ("Great job! Keep up the good work!", "https://example.com/image1.jpg"), //cat gacha place holder 1
            ("You did it! Stay strong!", "https://example.com/image2.jpg"), //cat gacha place holder 2
            ("Fantastic effort! Keep going!", "https://example.com/image3.jpg"), //cat gacha place holder 3
            ("Awesome! You're doing great!", "https://example.com/image4.jpg"), //cat gacha place holder 4
            ("Excellent! Keep pushing forward!", "https://example.com/image5.jpg") //cat gacha place holder 5
        };

        public IActionResult Index()
        {
            var today = DateTime.UtcNow.Date;
            var lastAccessDate = HttpContext.Session.GetString("LastAccessDate");
            var dailyChallenges = HttpContext.Session.GetString("DailyChallenges");
            var rewardClaimed = HttpContext.Session.GetString("RewardClaimed");

            if (string.IsNullOrEmpty(lastAccessDate) || DateTime.Parse(lastAccessDate) < today)
            {
                HttpContext.Session.SetString("LastAccessDate", today.ToString("yyyy-MM-dd"));
                HttpContext.Session.Remove("DailyChallenges");
                HttpContext.Session.Remove("RewardMessage");
                HttpContext.Session.Remove("RewardImageUrl");
                HttpContext.Session.Remove("RewardClaimed");

                dailyChallenges = null;
                rewardClaimed = null;
            }

            if (string.IsNullOrEmpty(dailyChallenges))
            {
                var randomChallenges = _challenges.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
                HttpContext.Session.SetString("DailyChallenges", string.Join(",", randomChallenges));
                dailyChallenges = string.Join(",", randomChallenges);
            }

            var selectedChallenges = dailyChallenges.Split(',').ToList();

            if (rewardClaimed == "true")
            {
                return View("Completed");
            }

            return View(selectedChallenges);
        }

        [HttpPost]
        public IActionResult SaveProgress([FromBody] List<string> challenges)
        {
            var randomReward = _rewards.OrderBy(x => Guid.NewGuid()).First();
            HttpContext.Session.SetString("RewardMessage", randomReward.Message);
            HttpContext.Session.SetString("RewardImageUrl", randomReward.ImageUrl);
            HttpContext.Session.SetString("RewardClaimed", "true");

            return Json(new { success = true, message = randomReward.Message, imageUrl = randomReward.ImageUrl });
        }

        [HttpGet]
        public IActionResult GetReward()
        {
            var rewardMessage = HttpContext.Session.GetString("RewardMessage");
            var rewardImageUrl = HttpContext.Session.GetString("RewardImageUrl");

            if (!string.IsNullOrEmpty(rewardMessage) && !string.IsNullOrEmpty(rewardImageUrl))
            {
                return Json(new { success = true, message = rewardMessage, imageUrl = rewardImageUrl });
            }

            return Json(new { success = false });
        }
    }
}
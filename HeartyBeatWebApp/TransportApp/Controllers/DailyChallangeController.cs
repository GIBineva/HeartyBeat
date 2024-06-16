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
            "Manage excess body fat",
            "Include fiber into your diet",
            "Make time for breakfast",
            "Eat fish",
            "Eat nuts",
            "Limit your salt intake",
            "Reduce your saturated fat intake",
            "Drink tea",
            "Eat dark chocolate",
            "Move your body throughout the day",
            "Practice yoga",
            "Try strength training",
            "Try interval training",
            "Try dancing",
            "Go for a walk",
            "Take the stairs",
            "Use housework as exercise",
            "Be a kid",
            "Engage in hobbies",
            "Laugh out loud",
            "Manage your stress",
            "Take the scenic route home"
        };

        public IActionResult Index()
        {
            var sessionKey = "DailyChallenges";
            var dateKey = "ChallengesDate";
            var currentDate = DateTime.Now.Date;

            var storedDate = HttpContext.Session.GetString(dateKey);
            var dailyChallenges = HttpContext.Session.GetString(sessionKey);

            if (storedDate == null || DateTime.Parse(storedDate) != currentDate || dailyChallenges == null)
            {
                var random = new Random();
                var newDailyChallenges = _challenges.OrderBy(x => random.Next()).Take(3).ToList();

                HttpContext.Session.SetString(sessionKey, JsonConvert.SerializeObject(newDailyChallenges));
                HttpContext.Session.SetString(dateKey, currentDate.ToString());

                dailyChallenges = JsonConvert.SerializeObject(newDailyChallenges);
            }

            var dailyChallengesList = JsonConvert.DeserializeObject<List<string>>(dailyChallenges);
            ViewBag.DailyChallenges = dailyChallengesList;

            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

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
            // Pick 3 random challenges
            var random = new Random();
            var dailyChallenges = _challenges.OrderBy(x => random.Next()).Take(3).ToList();

            // Pass the challenges to the view
            ViewBag.DailyChallenges = dailyChallenges;

            return View();
        }
    }
}

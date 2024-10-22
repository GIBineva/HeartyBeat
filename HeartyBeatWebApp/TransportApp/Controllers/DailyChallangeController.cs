﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using HeartyBeatApp.Models;
using HeartyBeatApp.Data;
using Microsoft.AspNetCore.Identity;
using HeartyBeat.Data;

namespace HeartyBeatApp.Controllers
{
    public class DailyChallengesController : Controller
    {
        private readonly List<string> _challenges = new List<string>
        {
                "Include fiber into your diet - Beans and Fruits and Oatmeal and Popcorn are foods full of fiber <3",
                "Make time for breakfast - The first meal for the day than can lead to positivity throughout the day!",
                "Eat fish - lowers blood pressure and help reduce the risk of a heart attack or stroke!!",
                "Eat nuts - good sources of protein healthy fats fibres vitamins and minerals!!",
                "Limit your salt intake - can cause high blood pressure!!",
                "Reduce your saturated fat intake - too much saturated fat can cause cholesterol to build up in your arteries.",
                "Drink tea - May improve blood pressure and has so much more benefits",
                "Eat dark chocolate - as tasty as milk chocolate but times more healthy <3",
                "Move your body throughout the day - WREK IT GIRL!!",
                "Practice yoga - all you need is a yoga mat and a YouTube video",
                "Try strength training - helps with stress and you can benefit from the results!!",
                "Try interval training - burns calories and reduces blood pressure",
                "Try dancing - fun way of working out!!",
                "Go for a walk - a nice way to clear your mind from stress",
                "Take the stairs - whoops the lift is suddenly broken.. wonder who did that...",
                "Use housework as exercise - clean house and happy body <3",
                "Be a kid - pfft who said this was for kids?? Go enjoy your life!!",
                "Engage in hobbies - a nice way to spend some me time",
                "Laugh out loud - who cares if people hear you? There are 5 billion people on earth",
                "Manage your stress - there are so many ways of that (breathing exercises and so much more)",
                "Take the scenic route home - explore your hometown or go through your fave route!!"
        };

        private readonly List<Reward> _rewards;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DailyChallengesController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _rewards = new List<Reward>
            {
                new Reward { Message = "Smirk smirk.. Looks like you got:", ImageUrl = "/images/Smirk.jpg" },
                new Reward { Message = "Nothing to say just :3", ImageUrl = "/images/meow.png" },
                new Reward { Message = "Uhh.. this is a little awkward", ImageUrl = "/images/Awkward.jpg" },
                new Reward { Message = "*twaek twaek*", ImageUrl = "/images/tweakin.jpg" },
                new Reward { Message = "Look at that pretty face you pulled:", ImageUrl = "/images/Realistic.jpg" },
                new Reward { Message = "Did you pull it or did it pull you?", ImageUrl = "/images/gigachad.jpg" },
                new Reward { Message = "*pew pew*", ImageUrl = "/images/Gangster.jpg" },
                new Reward { Message = "There is a new cat in town", ImageUrl = "/images/Cowboy.jpg" },
                new Reward { Message = "nom nom nom nom", ImageUrl = "/images/Munching.jpg" },
                new Reward { Message = "Remember it's always important to relax!", ImageUrl = "/images/Warm.jpg" },
                new Reward { Message = "Ohmygod!! :3", ImageUrl = "/images/HelloPulsey.jpg" },
                new Reward { Message = "Erm.. your heart rate is akshally high", ImageUrl = "/images/nerd.jpg" }
            };
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var today = DateTime.UtcNow.Date;
            var user = _userManager.GetUserId(User);
            var lastAccessDate = HttpContext.Session.GetString($"LastAccessDate_{user}");
            var dailyChallenges = HttpContext.Session.GetString($"DailyChallenges_{user}");
            var rewardClaimed = HttpContext.Session.GetString($"RewardClaimed_{user}");

            if (string.IsNullOrEmpty(lastAccessDate) || DateTime.Parse(lastAccessDate) < today)
            {
                HttpContext.Session.SetString($"LastAccessDate_{user}", today.ToString("yyyy-MM-dd"));
                HttpContext.Session.Remove($"DailyChallenges_{user}");
                HttpContext.Session.Remove($"RewardMessage_{user}");
                HttpContext.Session.Remove($"RewardImageUrl_{user}");
                HttpContext.Session.Remove($"RewardClaimed_{user}");

                dailyChallenges = null;
                rewardClaimed = null;
            }

            if (string.IsNullOrEmpty(dailyChallenges))
            {
                var randomChallenges = _challenges.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
                HttpContext.Session.SetString($"DailyChallenges_{user}", string.Join(",", randomChallenges));
                dailyChallenges = string.Join(",", randomChallenges);
            }

            var selectedChallenges = dailyChallenges.Split(',').ToList();

            if (rewardClaimed == "true")
            {
                return View("Completed");
            }

            ViewBag.DailyChallenges = selectedChallenges;
            return View(selectedChallenges);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProgress([FromBody] List<string> challenges)
        {
            var user = _userManager.GetUserId(User);
            var randomReward = _rewards.OrderBy(x => Guid.NewGuid()).First();
            HttpContext.Session.SetString($"RewardMessage_{user}", randomReward.Message);
            HttpContext.Session.SetString($"RewardImageUrl_{user}", randomReward.ImageUrl);
            HttpContext.Session.SetString($"RewardClaimed_{user}", "true");

            await InformCollectionControllerAboutRewardAsync(randomReward);

            return Json(new { success = true, message = randomReward.Message, imageUrl = Url.Content($"~{randomReward.ImageUrl}") });
        }

        [HttpGet]
        public IActionResult GetReward()
        {
            var user = _userManager.GetUserId(User);
            var rewardMessage = HttpContext.Session.GetString($"RewardMessage_{user}");
            var rewardImageUrl = HttpContext.Session.GetString($"RewardImageUrl_{user}");

            if (!string.IsNullOrEmpty(rewardMessage) && !string.IsNullOrEmpty(rewardImageUrl))
            {
                HttpContext.Session.Remove($"RewardMessage_{user}");
                HttpContext.Session.Remove($"RewardImageUrl_{user}");
                return Json(new { success = true, message = rewardMessage, imageUrl = Url.Content($"~{rewardImageUrl}") });
            }

            return Json(new { success = false });
        }

        private async Task InformCollectionControllerAboutRewardAsync(Reward reward)
        {
            var existingReward = _context.Reward.FirstOrDefault(r => r.Message == reward.Message);
            if (existingReward != null)
            {
                var user = await _userManager.GetUserAsync(User);
                user.Obtained.Add(existingReward);
                _context.SaveChanges();
            }
        }
    }
}
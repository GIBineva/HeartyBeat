using HeartyBeatApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace HeartyBeatApp.Repositories
{
    public class RewardRepository : IRewardRepository
    {
        private List<Reward> rewards = new List<Reward>
        {
                new Reward { Message = "Smirk smirk.. Looks like you got:", ImageUrl = "/images/Smirk.jpg" },
                new Reward { Message = "Nothing to say just :3", ImageUrl = "/images/meow.png" },
                new Reward { Message = "Uhh.. this is a little awkward", ImageUrl = "/images/Awkward.jpg" },
                new Reward { Message = "*twaek twaek*", ImageUrl = "/images/tweakin.jpg" },
                new Reward { Message = "Look at that pretty face you pulled:", ImageUrl = "/images/Realistic.jpg" },
                new Reward { Message = "Did you pull it or did it pull you?", ImageUrl = "/images/gigachad.jpg" },
                new Reward { Message = "*pew pew*", ImageUrl = "/images/Gangster.jpg" },
                new Reward { Message = "There is a new cat in town", ImageUrl = "/images/Cowboy.jpg" },
        };

        public List<Reward> GetAllRewards()
        {
            return rewards;
        }

        public bool UpdateRewardStatus(string message, bool obtained)
        {
            var reward = rewards.FirstOrDefault(r => r.Message == message);
            if (reward != null)
            {
                return true;
            }
            return false;
        }
    }
}

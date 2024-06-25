using HeartyBeatApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace HeartyBeatApp.Repositories
{
    public class RewardRepository : IRewardRepository
    {
        private List<Reward> rewards = new List<Reward>
        {
            new Reward { Message = "Great job! Keep up the good work!", ImageUrl = "/images/HeartCat1.jpg"},
            new Reward { Message = "You did it! Stay strong!", ImageUrl = "/images/HeartCat2.jpg"},
            new Reward { Message = "Fantastic effort! Keep going!", ImageUrl = "/images/HeartCat3.jpg"},
            new Reward { Message = "Awesome! You're doing great!", ImageUrl = "/images/HeartCat4.jpg"},
            new Reward { Message = "Excellent! Keep pushing forward!", ImageUrl = "/images/HeartCat5.jpg"}
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

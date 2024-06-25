using HeartyBeatApp.Models;
using HeartyBeatApp.Repositories;

namespace HeartyBeatApp.Services
{
    public class RewardService
    {
        private readonly IRewardRepository _rewardRepository;

        public RewardService(IRewardRepository rewardRepository)
        {
            _rewardRepository = rewardRepository;
        }

        public bool UpdateRewardStatus(string message, bool obtained)
        {
            return _rewardRepository.UpdateRewardStatus(message, obtained);
        }

        public List<Reward> GetAllRewards()
        {
            return _rewardRepository.GetAllRewards();
        }
    }
}

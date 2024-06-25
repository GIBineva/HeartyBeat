using HeartyBeatApp.Models;

namespace HeartyBeatApp.Repositories
{
    public interface IRewardRepository
    {
        List<Reward> GetAllRewards();
        bool UpdateRewardStatus(string message, bool obtained);
    }
}

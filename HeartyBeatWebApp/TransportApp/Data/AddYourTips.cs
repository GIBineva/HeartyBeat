using HeartyBeatApp.Data;

namespace HeartyBeat.Data
{
    public class AddYourTips:BaseEntity
    {
        public string TipFromUser { get; set; }
        public string Username { get; set; }
        public string? UserId { get; set; }
    }
}

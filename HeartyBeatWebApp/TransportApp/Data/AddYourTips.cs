using HeartyBeatApp.Data;

namespace HeartyBeat.Data
{
    public class AddYourTips:BaseEntity
    {
        public string tipFromUser { get; set; }
        public string username { get; set; }
        public string? UserId { get; set; }
    }
}

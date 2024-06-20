using HeartyBeatApp.Data;

namespace HeartyBeat.Data
{
    public class HealthyTIpsPersonal:BaseEntity
    {
        public string tipFromUser { get; set; }

        public string? UserId { get; set; }
    }
}

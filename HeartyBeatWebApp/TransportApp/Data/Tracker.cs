using HeartyBeatApp.Data;

namespace HeartyBeat.Data
{
    public class Tracker:BaseEntity
    {
        public int HeartRate { get; set; }
        public double Weight { get; set; }

        public double Height { get; set; }

        public int Steps { get; set; }

        public string? UserId { get; set; }
    }
}

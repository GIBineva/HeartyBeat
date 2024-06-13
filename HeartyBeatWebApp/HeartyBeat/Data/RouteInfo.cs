namespace HeartyBeatApp.Data
{
    public class RouteInfo : BaseEntity
    {
        public string Description { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }

        public RouteInfo() {
            Schedules = new HashSet<Schedule>();
        }
    }
}

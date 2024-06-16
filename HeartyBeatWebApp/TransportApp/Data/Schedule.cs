namespace HeartyBeatApp.Data
{
    public class Schedule : BaseEntity
    {
        public int LocationId { get; set; }
        public DateTime ExpectedTime { get; set; }
        public virtual RouteInfo? RouteInfo { get; set; }
        public int RouteInfoId { get; set; }
    }
}

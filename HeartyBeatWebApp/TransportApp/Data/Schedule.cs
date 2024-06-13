namespace TransportApp.Data
{
    public class Schedule : BaseEntity
    {
        public virtual Location? Location { get; set; }
        public int LocationId { get; set; }
        public DateTime ExpectedTime { get; set; }
        public virtual RouteInfo? RouteInfo { get; set; }
        public int RouteInfoId { get; set; }
    }
}

namespace TransportApp.Data
{
    public class Vehicle : BaseEntity
    {
        public string RegistrationPlate { get; set; }
        public VehicleType Type { get; set; }
        public int PassengerCount { get; set; }
        public virtual ICollection<RouteInfo> Routes { get; set; }

        public Vehicle()
        {
            Routes = new HashSet<RouteInfo>();
        }
    }
}

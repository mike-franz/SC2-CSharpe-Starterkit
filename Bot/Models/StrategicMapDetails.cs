namespace Bot.Models
{
    public class StrategicMapDetails
    {
        public StartingLocation StartingLocation { get; set; }

        public StartingLocation[] StartingLocations { get; set; }

        public StrategicPoint[] StrategicPoints { get; set; }
    }
}
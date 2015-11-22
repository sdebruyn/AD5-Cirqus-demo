namespace Oditel.Models.RoomContext
{
    public sealed class Bathroom
    {
        public Bathroom()
        {
        }

        public Bathroom(Dimensions dimensions, bool hasToilet, bool hasShower, bool hasBath, bool hasSink)
        {
            Dimensions = dimensions;
            HasToilet = hasToilet;
            HasShower = hasShower;
            HasBath = hasBath;
            HasSink = hasSink;
        }

        public Dimensions Dimensions { get; set; }
        public bool HasToilet { get; set; }
        public bool HasShower { get; set; }
        public bool HasBath { get; set; }
        public bool HasSink { get; set; }
    }
}
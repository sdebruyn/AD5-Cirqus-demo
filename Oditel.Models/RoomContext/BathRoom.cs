namespace Oditel.Models.RoomContext
{
    public sealed class Bathroom
    {
        public Dimensions Dimensions { get; set; }
        public bool HasToilet { get; set; }
        public bool HasShower { get; set; }
        public bool HasBath { get; set; }
        public bool HasSink { get; set; }
    }
}
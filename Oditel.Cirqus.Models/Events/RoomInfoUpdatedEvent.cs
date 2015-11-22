using d60.Cirqus.Events;
using Oditel.Models.RoomContext;

namespace Oditel.Cirqus.Models.Events
{
    public class RoomInfoUpdatedEvent : DomainEvent<Room>
    {
        public RoomInfoUpdatedEvent(bool tv, bool seperateToilet, Bathroom bathroom, Dimensions dimensions)
        {
            Tv = tv;
            SeperateToilet = seperateToilet;
            Bathroom = bathroom;
            Dimensions = dimensions;
        }

        public bool Tv { get; }
        public bool SeperateToilet { get; }
        public Bathroom Bathroom { get; }
        public Dimensions Dimensions { get; }
    }
}
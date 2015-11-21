using d60.Cirqus.Events;
using Oditel.Models;

namespace Oditel.Cirqus.Models.Events
{
    public class RoomInfoUpdatedEvent : DomainEvent<Room>
    {
        public RoomInfoUpdatedEvent(bool tv, bool seperateToilet, Bathroom bathroom)
        {
            Tv = tv;
            SeperateToilet = seperateToilet;
            Bathroom = bathroom;
        }

        public bool Tv { get; }
        public bool SeperateToilet { get; }
        public Bathroom Bathroom { get; }
    }
}
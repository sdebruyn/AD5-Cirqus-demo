using d60.Cirqus.Events;
using Oditel.Models.RoomContext;

namespace Oditel.Cirqus.Models.Events
{
    public class RoomBedAddedEvent : DomainEvent<Room>
    {
        public RoomBedAddedEvent(Bed bed)
        {
            Bed = bed;
        }

        public Bed Bed { get; }
    }
}
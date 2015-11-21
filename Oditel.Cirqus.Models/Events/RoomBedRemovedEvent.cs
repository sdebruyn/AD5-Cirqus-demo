using d60.Cirqus.Events;
using Oditel.Models;
using Oditel.Models.RoomContext;

namespace Oditel.Cirqus.Models.Events
{
    public class RoomBedRemovedEvent : DomainEvent<Room>
    {
        public RoomBedRemovedEvent(Bed bed)
        {
            Bed = bed;
        }

        public Bed Bed { get; }
    }
}
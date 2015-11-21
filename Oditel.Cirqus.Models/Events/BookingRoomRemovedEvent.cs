using System;
using d60.Cirqus.Events;

namespace Oditel.Cirqus.Models.Events
{
    public class BookingRoomRemovedEvent : DomainEvent<Booking>
    {
        public BookingRoomRemovedEvent(Guid roomId)
        {
            RoomId = roomId;
        }

        public Guid RoomId { get; }
    }
}
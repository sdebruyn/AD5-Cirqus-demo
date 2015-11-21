using System;
using d60.Cirqus.Events;

namespace Oditel.Cirqus.Models.Events
{
    public class BookingRoomAddedEvent : DomainEvent<Booking>
    {
        public BookingRoomAddedEvent(Guid roomId)
        {
            RoomId = roomId;
        }

        public Guid RoomId { get; }
    }
}
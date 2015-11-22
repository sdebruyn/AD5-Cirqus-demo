using System;
using d60.Cirqus.Events;

namespace Oditel.Cirqus.Models.Events
{
    public class BookingRoomRemovedEvent : DomainEvent<Booking>
    {
        public BookingRoomRemovedEvent(Guid roomId, DateTimeOffset checkInDate)
        {
            RoomId = roomId;
            CheckInDate = checkInDate;
        }

        public Guid RoomId { get; }
        public DateTimeOffset CheckInDate { get; }
    }
}
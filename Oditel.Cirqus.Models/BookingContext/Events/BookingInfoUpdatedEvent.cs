using System;
using d60.Cirqus.Events;

namespace Oditel.Cirqus.Models.BookingContext.Events
{
    public class BookingInfoUpdatedEvent : DomainEvent<Booking>
    {
        public BookingInfoUpdatedEvent(DateTimeOffset checkIn, DateTimeOffset checkOut, bool paid, Guid customerId)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            Paid = paid;
            CustomerId = customerId;
        }

        public DateTimeOffset CheckIn { get; }
        public DateTimeOffset CheckOut { get; }
        public bool Paid { get; }
        public Guid CustomerId { get; }
    }
}
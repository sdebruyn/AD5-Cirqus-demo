using System;
using System.Collections.Generic;

namespace Oditel.Models.BookingContext
{
    public interface IBooking
    {
        Guid? BookingId { get; }
        DateTimeOffset? CreatedDate { get; set; }
        DateTimeOffset? DeletedDate { get; set; }
        DateTimeOffset CheckIn { get; }
        DateTimeOffset CheckOut { get; }
        bool Paid { get; }
        Guid CustomerId { get; }
        ICollection<Guid> Rooms { get; }
        void UpdateInfo(DateTimeOffset checkIn, DateTimeOffset checkOut, bool paid, Guid customerId);
        void AddRoom(Guid roomId);
        void RemoveRoom(Guid roomId);
    }
}
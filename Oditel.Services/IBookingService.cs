using System;
using System.Collections.Generic;
using Oditel.Models.BookingContext;

namespace Oditel.Services
{
    public interface IBookingService
    {
        Guid AddBooking(IBooking booking);

        Guid AddBooking(DateTimeOffset checkIn, DateTimeOffset checkOut, bool paid, Guid customerId,
            params Guid[] rooms);

        bool RemoveBooking(Guid bookingId);
        ICollection<IBooking> GetAllBookings();
        IBooking GetBookingById(Guid bookingId);
    }
}
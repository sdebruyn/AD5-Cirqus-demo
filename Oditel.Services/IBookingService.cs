using System;
using System.Collections.Generic;
using Oditel.Models;

namespace Oditel.Services
{
    public interface IBookingService
    {
        Guid AddBooking(IBooking booking);
        bool RemoveBooking(Guid bookingId);
        IEnumerable<IBooking> GetAllBookings();
        IBooking GetBookingById(Guid bookingId);
    }
}
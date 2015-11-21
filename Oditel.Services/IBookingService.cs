using System;
using Oditel.Models;

namespace Oditel.Services
{
    public interface IBookingService
    {
        Guid AddBooking(IBooking booking);
        bool RemoveBooking(Guid bookingId);
    }
}
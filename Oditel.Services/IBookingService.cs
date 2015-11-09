using Oditel.Models;

namespace Oditel.Services
{
    public interface IBookingService
    {
        void AddBooking(IBooking booking);
        void RemoveBooking(string bookingId);
    }
}
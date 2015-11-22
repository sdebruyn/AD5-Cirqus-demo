using System;
using System.Collections.Generic;
using System.Linq;
using d60.Cirqus;
using Oditel.Cirqus.Models.BaseContext.Exceptions;
using Oditel.Cirqus.Models.BookingContext.Commands;
using Oditel.Cirqus.Views;
using Oditel.Models.BookingContext;
using Oditel.Services;

namespace Oditel.Cirqus.Services
{
    public class BookingService : IBookingService
    {
        private readonly IQueryable<BookingView> _bookings;

        private readonly ICommandProcessor _processor;

        public BookingService(ICommandProcessor processor, IQueryable<BookingView> bookings)
        {
            _bookings = bookings;
            _processor = processor;
        }

        private IList<BookingView> Bookings
            => _bookings.Where(b => b.DeletedDate == null && b.CreatedDate != null).ToList();

        public Guid AddBooking(IBooking booking)
        {
            var command = new AddBookingCommand(booking);
            var result = _processor.ProcessCommand(command);
            if (result.EventsWereEmitted)
            {
                return command.CreatedGuid;
            }
            throw new CreationFailedException(command.CreatedGuid, typeof (IBooking));
        }

        public Guid AddBooking(DateTimeOffset checkIn, DateTimeOffset checkOut, bool paid, Guid customerId,
            params Guid[] rooms)
        {
            var command = new AddBookingCommand(checkIn, checkOut, paid, customerId, rooms);
            var result = _processor.ProcessCommand(command);
            if (result.EventsWereEmitted)
            {
                return command.CreatedGuid;
            }
            throw new CreationFailedException(command.CreatedGuid, typeof (IBooking));
        }

        public bool RemoveBooking(Guid bookingId)
            => _processor.ProcessCommand(new RemoveBookingCommand(bookingId)).EventsWereEmitted;

        public ICollection<IBooking> GetAllBookings() => Bookings.Select(b => b.GetBookingFromView()).ToList();

        public IBooking GetBookingById(Guid bookingId)
        {
            try
            {
                return Bookings.Single(b => b.BookingId == bookingId).GetBookingFromView();
            }
            catch (Exception)
            {
                throw new LoadingFailedException(bookingId, typeof (IBooking));
            }
        }
    }
}
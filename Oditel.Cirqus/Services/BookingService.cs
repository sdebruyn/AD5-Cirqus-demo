using System;
using System.Collections.Generic;
using d60.Cirqus;
using Oditel.Cirqus.Models.Commands;
using Oditel.Cirqus.Models.Exceptions;
using Oditel.Models;
using Oditel.Models.BookingContext;
using Oditel.Services;

namespace Oditel.Cirqus.Services
{
    public class BookingService : IBookingService
    {
        private readonly ICommandProcessor _processor;

        public BookingService(ICommandProcessor processor)
        {
            _processor = processor;
        }

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

        public bool RemoveBooking(Guid bookingId)
            => _processor.ProcessCommand(new RemoveBookingCommand(bookingId)).EventsWereEmitted;

        public IEnumerable<IBooking> GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public IBooking GetBookingById(Guid bookingId)
        {
            throw new NotImplementedException();
        }
    }
}
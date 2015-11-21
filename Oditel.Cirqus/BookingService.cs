using System;
using d60.Cirqus;
using Oditel.Cirqus.Models.Commands;
using Oditel.Cirqus.Models.Exceptions;
using Oditel.Models;
using Oditel.Services;

namespace Oditel.Cirqus
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
    }
}
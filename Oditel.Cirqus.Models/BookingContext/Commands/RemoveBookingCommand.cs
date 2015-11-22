using System;
using Oditel.Cirqus.Models.BaseContext.Commands;

namespace Oditel.Cirqus.Models.BookingContext.Commands
{
    public class RemoveBookingCommand : UpdateCommand<Booking>
    {
        public RemoveBookingCommand(Guid bookingId) : base(bookingId)
        {
        }

        protected override void Update(Booking instance)
        {
            instance.DeletedDate = DateTimeOffset.UtcNow;
        }
    }
}
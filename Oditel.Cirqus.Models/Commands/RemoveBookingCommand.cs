using System;

namespace Oditel.Cirqus.Models.Commands
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
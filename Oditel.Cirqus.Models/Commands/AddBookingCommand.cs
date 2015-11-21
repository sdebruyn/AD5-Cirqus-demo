using System;
using System.Collections.Generic;
using System.Linq;
using Oditel.Models;

namespace Oditel.Cirqus.Models.Commands
{
    public class AddBookingCommand : CreateCommand<Booking>
    {
        private readonly DateTimeOffset _checkIn;
        private readonly DateTimeOffset _checkOut;
        private readonly Guid _customerId;
        private readonly bool _paid;
        private readonly List<Guid> _rooms;

        public AddBookingCommand(DateTimeOffset checkIn, DateTimeOffset checkOut, bool paid, Guid customerId,
            params Guid[] rooms)
        {
            _checkIn = checkIn;
            _checkOut = checkOut;
            _paid = paid;
            _customerId = customerId;
            _rooms = new List<Guid>(rooms);
        }

        public AddBookingCommand(IBooking booking)
            : this(booking.CheckIn, booking.CheckOut, booking.Paid, booking.CustomerId, booking.Rooms.ToArray())
        {
        }

        protected override void Update(Booking instance)
        {
            instance.UpdateInfo(_checkIn, _checkOut, _paid, _customerId);
            _rooms.ForEach(instance.AddRoom);
        }
    }
}
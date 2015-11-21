using System;
using System.Collections.Generic;
using Oditel.Models;

namespace Oditel.Cirqus.Views.Models
{
    internal class Booking : IBooking
    {
        private readonly List<Guid> _rooms;

        public Booking(Guid id)
        {
            BookingId = id;
            _rooms = new List<Guid>();
        }

        public Guid? BookingId { get; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public DateTimeOffset CheckIn { get; private set; }
        public DateTimeOffset CheckOut { get; private set; }
        public bool Paid { get; private set; }
        public Guid CustomerId { get; private set; }

        public IEnumerable<Guid> Rooms => _rooms;

        public void UpdateInfo(DateTimeOffset checkIn, DateTimeOffset checkOut, bool paid, Guid customerId)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            Paid = paid;
            CustomerId = customerId;
        }

        public void AddRoom(Guid roomId)
        {
            _rooms.Add(roomId);
        }

        public void RemoveRoom(Guid roomId)
        {
            _rooms.Remove(roomId);
        }
    }
}
using System;
using System.Collections.Generic;
using d60.Cirqus.Events;
using Oditel.Cirqus.Models.BaseContext;
using Oditel.Cirqus.Models.BaseContext.Events;
using Oditel.Cirqus.Models.BookingContext.Events;
using Oditel.Models.BookingContext;

namespace Oditel.Cirqus.Models.BookingContext
{
    public class Booking : AggregateRootBase, IBooking,
        IEmit<AggregateRootCreatedEvent<Booking>>,
        IEmit<AggregateRootDeletedEvent<Booking>>,
        IEmit<BookingInfoUpdatedEvent>,
        IEmit<BookingRoomAddedEvent>,
        IEmit<BookingRoomRemovedEvent>
    {
        private readonly IList<Guid> _rooms;
        private DateTimeOffset? _createdDate;
        private DateTimeOffset? _deletedDate;

        public Booking()
        {
            _rooms = new List<Guid>();
        }

        public override DateTimeOffset? CreatedDate
        {
            get { return _createdDate; }
            set { Emit(new AggregateRootCreatedEvent<Booking>(value)); }
        }

        public override DateTimeOffset? DeletedDate
        {
            get { return _deletedDate; }
            set
            {
                foreach (var room in Rooms)
                {
                    Emit(new BookingRoomRemovedEvent(room, CheckIn));
                }
                Emit(new AggregateRootDeletedEvent<Booking>(value));
            }
        }

        public Guid? BookingId => ConvertIdToGuid();
        public DateTimeOffset CheckIn { get; private set; }
        public DateTimeOffset CheckOut { get; private set; }
        public bool Paid { get; private set; }
        public Guid CustomerId { get; private set; }

        public ICollection<Guid> Rooms => _rooms;

        public void UpdateInfo(DateTimeOffset checkIn, DateTimeOffset checkOut, bool paid, Guid customerId)
        {
            ThrowIfDeleted();
            Emit(new BookingInfoUpdatedEvent(checkIn, checkOut, paid, customerId));
        }

        public void AddRoom(Guid roomId)
        {
            ThrowIfDeleted();
            Emit(new BookingRoomAddedEvent(roomId, CheckIn));
        }

        public void RemoveRoom(Guid roomId)
        {
            Emit(new BookingRoomRemovedEvent(roomId, CheckIn));
        }

        public void Apply(AggregateRootCreatedEvent<Booking> e)
        {
            _createdDate = e.CreatedDate;
        }

        public void Apply(AggregateRootDeletedEvent<Booking> e)
        {
            _deletedDate = e.DeletedDate;
        }

        public void Apply(BookingInfoUpdatedEvent e)
        {
            ThrowIfDeleted();

            CheckIn = e.CheckIn;
            CheckOut = e.CheckOut;
            Paid = e.Paid;
            CustomerId = e.CustomerId;
        }

        public void Apply(BookingRoomAddedEvent e)
        {
            ThrowIfDeleted();
            _rooms.Add(e.RoomId);
        }

        public void Apply(BookingRoomRemovedEvent e)
        {
            ThrowIfDeleted();
            _rooms.Remove(e.RoomId);
        }
    }
}
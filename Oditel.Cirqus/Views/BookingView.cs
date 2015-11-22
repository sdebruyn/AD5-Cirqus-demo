using System;
using System.Collections.Generic;
using d60.Cirqus.Extensions;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using Newtonsoft.Json;
using Oditel.Cirqus.Models;
using Oditel.Cirqus.Models.Events;
using Oditel.Models.BookingContext;

namespace Oditel.Cirqus.Views
{
    public class BookingView : IViewInstance<BookingView.Locator>,
        ISubscribeTo<BookingInfoUpdatedEvent>,
        ISubscribeTo<AggregateRootCreatedEvent<Booking>>,
        ISubscribeTo<AggregateRootDeletedEvent<Booking>>,
        ISubscribeTo<BookingRoomAddedEvent>,
        ISubscribeTo<BookingRoomRemovedEvent>
    {
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public Guid BookingId => Guid.Parse(Id);
        public string SerializedRooms { get; set; }

        public Guid CustomerId { get; set; }

        public bool Paid { get; set; }

        public DateTimeOffset CheckOut { get; set; }

        public DateTimeOffset CheckIn { get; set; }

        public void Handle(IViewContext context, AggregateRootCreatedEvent<Booking> domainEvent)
        {
            CreatedDate = domainEvent.CreatedDate;
        }

        public void Handle(IViewContext context, AggregateRootDeletedEvent<Booking> domainEvent)
        {
            DeletedDate = domainEvent.DeletedDate;
        }

        public void Handle(IViewContext context, BookingInfoUpdatedEvent domainEvent)
        {
            CheckIn = domainEvent.CheckIn;
            CheckOut = domainEvent.CheckOut;
            CustomerId = domainEvent.CustomerId;
            Paid = domainEvent.Paid;
        }

        public void Handle(IViewContext context, BookingRoomAddedEvent domainEvent)
        {
            var list = GetRooms();
            list.Add(domainEvent.RoomId);
            SetRooms(list);
        }

        public void Handle(IViewContext context, BookingRoomRemovedEvent domainEvent)
        {
            var list = GetRooms();
            list.Remove(domainEvent.RoomId);
            SetRooms(list);
        }

        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }

        public IBooking GetBookingFromView()
        {
            var booking = new Models.Booking(Guid.Parse(Id))
            {
                CreatedDate = CreatedDate,
                DeletedDate = DeletedDate
            };
            booking.UpdateInfo(CheckIn, CheckOut, Paid, CustomerId);
            GetRooms().ForEach(booking.AddRoom);

            return booking;
        }

        private void SetRooms(IEnumerable<Guid> rooms)
        {
            SerializedRooms = JsonConvert.SerializeObject(rooms);
        }

        private List<Guid> GetRooms() => string.IsNullOrWhiteSpace(SerializedRooms) ? new List<Guid>() : JsonConvert.DeserializeObject<List<Guid>>(SerializedRooms);

        private class Locator : HandlerViewLocator,
            IGetViewIdsFor<AggregateRootCreatedEvent<Booking>>,
            IGetViewIdsFor<AggregateRootDeletedEvent<Booking>>,
            IGetViewIdsFor<BookingInfoUpdatedEvent>,
            IGetViewIdsFor<BookingRoomAddedEvent>,
            IGetViewIdsFor<BookingRoomRemovedEvent>
        {
            public IEnumerable<string> GetViewIds(IViewContext context, AggregateRootCreatedEvent<Booking> e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, AggregateRootDeletedEvent<Booking> e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, BookingInfoUpdatedEvent e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, BookingRoomAddedEvent e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, BookingRoomRemovedEvent e)
            {
                yield return e.GetAggregateRootId();
            }
        }
    }
}
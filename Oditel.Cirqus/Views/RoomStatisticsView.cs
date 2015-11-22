using System;
using System.Collections.Generic;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using Oditel.Cirqus.Models.BookingContext.Events;

namespace Oditel.Cirqus.Views
{
    public class RoomStatisticsView : IViewInstance<RoomStatisticsView.Locator>,
        ISubscribeTo<BookingRoomAddedEvent>,
        ISubscribeTo<BookingRoomRemovedEvent>
    {
        public int BookingsInFirstQuarter { get; set; }
        public int BookingsInSecondQuarter { get; set; }
        public int BookingsInThirdQuarter { get; set; }
        public int BookingsInFourthQuarter { get; set; }

        public void Handle(IViewContext context, BookingRoomAddedEvent domainEvent)
        {
            switch (GetQuarter(domainEvent.CheckInDate))
            {
                case 1:
                    BookingsInFirstQuarter++;
                    break;
                case 2:
                    BookingsInSecondQuarter++;
                    break;
                case 3:
                    BookingsInThirdQuarter++;
                    break;
                case 4:
                    BookingsInFourthQuarter++;
                    break;
                default:
                    throw new InvalidOperationException("Quarter out of bounds.");
            }
        }

        public void Handle(IViewContext context, BookingRoomRemovedEvent domainEvent)
        {
            switch (GetQuarter(domainEvent.CheckInDate))
            {
                case 1:
                    BookingsInFirstQuarter--;
                    break;
                case 2:
                    BookingsInSecondQuarter--;
                    break;
                case 3:
                    BookingsInThirdQuarter--;
                    break;
                case 4:
                    BookingsInFourthQuarter--;
                    break;
                default:
                    throw new InvalidOperationException("Quarter out of bounds.");
            }
        }

        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }

        private static byte GetQuarter(DateTimeOffset checkInDate) => (byte) ((checkInDate.Month/4) + 1);

        private class Locator : HandlerViewLocator,
            IGetViewIdsFor<BookingRoomAddedEvent>,
            IGetViewIdsFor<BookingRoomRemovedEvent>
        {
            public IEnumerable<string> GetViewIds(IViewContext context, BookingRoomAddedEvent e)
            {
                yield return e.RoomId.ToString();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, BookingRoomRemovedEvent e)
            {
                yield return e.RoomId.ToString();
            }
        }
    }
}
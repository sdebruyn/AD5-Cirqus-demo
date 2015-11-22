using System;
using System.Collections.Generic;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using Oditel.Cirqus.Models.Events;

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
            UpdateQuarter(q => q++);
        }

        public void Handle(IViewContext context, BookingRoomRemovedEvent domainEvent)
        {
            UpdateQuarter(q => q--);
        }

        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }

        private byte GetQuarter() => (byte) ((DateTimeOffset.UtcNow.Month/4) + 1);

        private void UpdateQuarter(Action<int> updateAction)
        {
            switch (GetQuarter())
            {
                case 1:
                    updateAction(BookingsInFirstQuarter);
                    break;
                case 2:
                    updateAction(BookingsInSecondQuarter);
                    break;
                case 3:
                    updateAction(BookingsInThirdQuarter);
                    break;
                case 4:
                    updateAction(BookingsInFourthQuarter);
                    break;
                default:
                    throw new InvalidOperationException("Quarter out of bounds.");
            }
        }

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
using System.Collections.Generic;
using d60.Cirqus.Extensions;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using Oditel.Cirqus.Models;
using Oditel.Cirqus.Models.Events;

namespace Oditel.Cirqus.Views
{
    public class BookingView: IViewInstance<BookingView.Locator>
    {
        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }

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
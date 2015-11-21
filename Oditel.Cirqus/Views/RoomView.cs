using System.Collections.Generic;
using d60.Cirqus.Extensions;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using Oditel.Cirqus.Models;
using Oditel.Cirqus.Models.Events;

namespace Oditel.Cirqus.Views
{
    public class RoomView: IViewInstance<RoomView.Locator>
    {
        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }

        private class Locator : HandlerViewLocator,
            IGetViewIdsFor<AggregateRootCreatedEvent<Room>>,
            IGetViewIdsFor<AggregateRootDeletedEvent<Room>>,
            IGetViewIdsFor<RoomInfoUpdatedEvent>,
            IGetViewIdsFor<RoomBedAddedEvent>,
            IGetViewIdsFor<RoomBedRemovedEvent>
        {
            public IEnumerable<string> GetViewIds(IViewContext context, AggregateRootCreatedEvent<Room> e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, AggregateRootDeletedEvent<Room> e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, RoomInfoUpdatedEvent e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, RoomBedAddedEvent e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, RoomBedRemovedEvent e)
            {
                yield return e.GetAggregateRootId();
            }
        }
    }
}
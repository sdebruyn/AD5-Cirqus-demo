using System.Collections.Generic;
using d60.Cirqus.Extensions;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using Oditel.Cirqus.Models;
using Oditel.Cirqus.Models.Events;

namespace Oditel.Cirqus.Views
{
    public class CustomerView: IViewInstance<CustomerView.Locator>
    {
        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }

        private class Locator : HandlerViewLocator,
            IGetViewIdsFor<AggregateRootCreatedEvent<Customer>>,
            IGetViewIdsFor<AggregateRootDeletedEvent<Customer>>,
            IGetViewIdsFor<CustomerInfoUpdatedEvent>
        {
            public IEnumerable<string> GetViewIds(IViewContext context, AggregateRootCreatedEvent<Customer> e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, AggregateRootDeletedEvent<Customer> e)
            {
                yield return e.GetAggregateRootId();
            }

            public IEnumerable<string> GetViewIds(IViewContext context, CustomerInfoUpdatedEvent e)
            {
                yield return e.GetAggregateRootId();
            }
        }
    }
}
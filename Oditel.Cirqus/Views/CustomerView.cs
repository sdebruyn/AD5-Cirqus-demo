using System;
using System.Collections.Generic;
using d60.Cirqus.Extensions;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using Newtonsoft.Json;
using Oditel.Cirqus.Models;
using Oditel.Cirqus.Models.Events;
using Oditel.Models;
using Oditel.Models.CustomerContext;

namespace Oditel.Cirqus.Views
{
    public class CustomerView : IViewInstance<CustomerView.Locator>,
        ISubscribeTo<AggregateRootCreatedEvent<Customer>>,
        ISubscribeTo<AggregateRootDeletedEvent<Customer>>,
        ISubscribeTo<CustomerInfoUpdatedEvent>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string SerializedAddress { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }

        public Guid CustomerId => Guid.Parse(Id);

        public void Handle(IViewContext context, AggregateRootCreatedEvent<Customer> domainEvent)
        {
            CreatedDate = domainEvent.CreatedDate;
        }

        public void Handle(IViewContext context, AggregateRootDeletedEvent<Customer> domainEvent)
        {
            DeletedDate = domainEvent.DeletedDate;
        }

        public void Handle(IViewContext context, CustomerInfoUpdatedEvent domainEvent)
        {
            Name = domainEvent.Name;
            Email = domainEvent.Email;
            SetAddress(domainEvent.Address);
        }

        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }

        public ICustomer GetCustomerFromView()
        {
            var customer = new Models.Customer(Guid.Parse(Id))
            {
                CreatedDate = CreatedDate,
                DeletedDate = DeletedDate
            };

            customer.UpdateInfo(Name, Email, GetAddress());
            return customer;
        }

        private Address GetAddress() => JsonConvert.DeserializeObject<Address>(SerializedAddress);

        private void SetAddress(Address address)
        {
            SerializedAddress = JsonConvert.SerializeObject(address);
        }

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
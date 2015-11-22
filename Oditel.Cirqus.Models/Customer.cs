using System;
using d60.Cirqus.Events;
using Oditel.Cirqus.Models.Events;
using Oditel.Models.CustomerContext;

namespace Oditel.Cirqus.Models
{
    public class Customer : AggregateRootBase, ICustomer,
        IEmit<AggregateRootCreatedEvent<Customer>>,
        IEmit<AggregateRootDeletedEvent<Customer>>,
        IEmit<CustomerInfoUpdatedEvent>
    {
        private DateTimeOffset? _createdDate;
        private DateTimeOffset? _deletedDate;

        public override DateTimeOffset? CreatedDate
        {
            get { return _createdDate; }
            set { Emit(new AggregateRootCreatedEvent<Customer>(value)); }
        }

        public override DateTimeOffset? DeletedDate
        {
            get { return _deletedDate; }
            set { Emit(new AggregateRootDeletedEvent<Customer>(value)); }
        }

        public Guid? CustomerId => ConvertIdToGuid();
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public string Email { get; private set; }

        public void UpdateInfo(string name, string email, Address address)
        {
            ThrowIfDeleted();
            Emit(new CustomerInfoUpdatedEvent(name, email, address));
        }

        public void Apply(AggregateRootCreatedEvent<Customer> e)
        {
            _createdDate = e.CreatedDate;
        }

        public void Apply(AggregateRootDeletedEvent<Customer> e)
        {
            _deletedDate = e.DeletedDate;
        }

        public void Apply(CustomerInfoUpdatedEvent e)
        {
            ThrowIfDeleted();

            Address = e.Address;
            Email = e.Email;
            Name = e.Name;
        }
    }
}
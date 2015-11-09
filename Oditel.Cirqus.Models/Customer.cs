using System;
using d60.Cirqus.Events;
using Oditel.Cirqus.Models.Events;
using Oditel.Models;

namespace Oditel.Cirqus.Models
{
    public class Customer: AggregateRootBase, ICustomer,
        IEmit<AggregateRootCreatedEvent<Customer>>,
        IEmit<AggregateRootDeletedEvent<Customer>>
    {
        private DateTimeOffset? _createdDate;
        private DateTimeOffset? _deletedDate;

        public override DateTimeOffset? CreatedDate
        {
            get { return _createdDate; }
            set { Emit(new AggregateRootDeletedEvent<Customer>(value)); }
        }

        public override DateTimeOffset? DeletedDate
        {
            get { return _deletedDate; }
            set { Emit(new AggregateRootCreatedEvent<Customer>(value)); }
        }

        public void Apply(AggregateRootCreatedEvent<Customer> e)
        {
            _createdDate = e.CreatedDate;
        }

        public void Apply(AggregateRootDeletedEvent<Customer> e)
        {
            _deletedDate = e.DeletedDate;
        }
    }
}
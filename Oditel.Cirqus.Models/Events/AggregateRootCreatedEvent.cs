using System;
using d60.Cirqus.Events;

namespace Oditel.Cirqus.Models.Events
{
    public class AggregateRootCreatedEvent<TOwner> : DomainEvent<TOwner> where TOwner : AggregateRootBase
    {
        public AggregateRootCreatedEvent(DateTimeOffset? createdDate)
        {
            CreatedDate = createdDate;
        }

        public DateTimeOffset? CreatedDate { get; }
    }
}
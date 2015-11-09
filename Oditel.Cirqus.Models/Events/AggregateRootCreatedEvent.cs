using System;
using d60.Cirqus.Events;

namespace Oditel.Cirqus.Models.Events
{
    public class AggregateRootCreatedEvent<TOwner>: DomainEvent<TOwner> where TOwner : AggregateRootBase
    {
        public DateTimeOffset? CreatedDate { get; }

        public AggregateRootCreatedEvent(DateTimeOffset? createdDate)
        {
            CreatedDate = createdDate;
        }
    }
}
using System;
using d60.Cirqus.Events;

namespace Oditel.Cirqus.Models.Events
{
    public class AggregateRootDeletedEvent<TOwner>: DomainEvent<TOwner> where TOwner : AggregateRootBase
    {
        public DateTimeOffset? DeletedDate { get; }

        public AggregateRootDeletedEvent(DateTimeOffset? deletedDate)
        {
            DeletedDate = deletedDate;
        }
    }
}
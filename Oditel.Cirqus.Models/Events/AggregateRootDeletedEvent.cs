using System;
using d60.Cirqus.Events;

namespace Oditel.Cirqus.Models.Events
{
    public class AggregateRootDeletedEvent<TOwner> : DomainEvent<TOwner> where TOwner : AggregateRootBase
    {
        public AggregateRootDeletedEvent(DateTimeOffset? deletedDate)
        {
            DeletedDate = deletedDate;
        }

        public DateTimeOffset? DeletedDate { get; }
    }
}
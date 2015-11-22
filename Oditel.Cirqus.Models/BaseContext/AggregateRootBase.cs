using System;
using d60.Cirqus.Aggregates;

namespace Oditel.Cirqus.Models.BaseContext
{
    public abstract class AggregateRootBase : AggregateRoot
    {
        public abstract DateTimeOffset? CreatedDate { get; set; }
        public abstract DateTimeOffset? DeletedDate { get; set; }

        protected override void Created()
        {
            base.Created();
            CreatedDate = DateTimeOffset.UtcNow;
        }

        public void ThrowIfDeleted()
        {
            if (DeletedDate.HasValue)
            {
                throw new InvalidOperationException("This object has been deleted.");
            }
        }

        public Guid? ConvertIdToGuid()
        {
            Guid parsed;
            return Guid.TryParse(Id, out parsed) ? (Guid?) parsed : null;
        }
    }
}
using System;
using d60.Cirqus.Commands;
using Oditel.Cirqus.Models.Exceptions;

namespace Oditel.Cirqus.Models.Commands
{
    public abstract class UpdateCommand<TAggregateRoot> : ExecutableCommand
        where TAggregateRoot : AggregateRootBase, new()
    {
        private readonly Guid _aggregateRootId;

        protected UpdateCommand(Guid aggregateRootId)
        {
            _aggregateRootId = aggregateRootId;
        }

        public override sealed void Execute(ICommandContext context)
        {
            var instance = context.TryLoad<TAggregateRoot>(_aggregateRootId.ToString());
            if (instance == null)
            {
                throw new LoadingFailedException(_aggregateRootId, typeof (TAggregateRoot));
            }
            Update(instance);
        }

        protected abstract void Update(TAggregateRoot instance);
    }
}
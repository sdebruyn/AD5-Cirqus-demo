using System;
using d60.Cirqus.Commands;
using Oditel.Cirqus.Models.BaseContext.Exceptions;

namespace Oditel.Cirqus.Models.BaseContext.Commands
{
    public abstract class CreateCommand<TAggregateRoot> : ExecutableCommand, ICreateCommand
        where TAggregateRoot : AggregateRootBase, new()
    {
        protected CreateCommand()
        {
            CreatedGuid = Guid.NewGuid();
        }

        public Guid CreatedGuid { get; }

        public override sealed void Execute(ICommandContext context)
        {
            TAggregateRoot instance;
            try
            {
                instance = context.Create<TAggregateRoot>(CreatedGuid.ToString());
            }
            catch (InvalidOperationException)
            {
                throw new CreationFailedException(CreatedGuid, typeof (TAggregateRoot));
            }

            Update(instance);
        }

        protected abstract void Update(TAggregateRoot instance);
    }
}
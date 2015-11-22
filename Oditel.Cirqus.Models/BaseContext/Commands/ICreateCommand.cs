using System;

namespace Oditel.Cirqus.Models.BaseContext.Commands
{
    public interface ICreateCommand
    {
        Guid CreatedGuid { get; }
    }
}
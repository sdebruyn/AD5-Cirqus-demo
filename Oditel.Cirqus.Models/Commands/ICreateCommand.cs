using System;

namespace Oditel.Cirqus.Models.Commands
{
    public interface ICreateCommand
    {
        Guid CreatedGuid { get; }
    }
}
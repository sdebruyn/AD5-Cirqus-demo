using System;
using System.Runtime.Serialization;

namespace Oditel.Cirqus.Models.BaseContext.Exceptions
{
    [Serializable]
    public abstract class ActionFailedException : Exception
    {
        protected ActionFailedException(Guid id, Type type, string message = "was not ready to process")
            : base($"{type} with ID {id} {message}.")
        {
            Id = id;
            Type = type;
        }

        protected ActionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Id = (Guid) info.GetValue(nameof(Id), typeof (Guid));
            Type = (Type) info.GetValue(nameof(Type), typeof (Type));
        }

        public Guid Id { get; }
        public Type Type { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Type), Type);
        }
    }
}
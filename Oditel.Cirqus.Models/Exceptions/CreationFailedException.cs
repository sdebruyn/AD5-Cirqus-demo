using System;
using System.Runtime.Serialization;

namespace Oditel.Cirqus.Models.Exceptions
{
    [Serializable]
    public class CreationFailedException : ActionFailedException
    {
        public CreationFailedException(Guid id, Type type) : base(id, type, "could not be created")
        {
        }

        protected CreationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
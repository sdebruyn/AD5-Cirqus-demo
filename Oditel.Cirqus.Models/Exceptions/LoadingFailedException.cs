using System;
using System.Runtime.Serialization;

namespace Oditel.Cirqus.Models.Exceptions
{
    [Serializable]
    public class LoadingFailedException : ActionFailedException
    {
        public LoadingFailedException(Guid id, Type type) : base(id, type, "not found")
        {
        }

        protected LoadingFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
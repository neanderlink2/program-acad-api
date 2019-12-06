using System;

namespace ProgramAcad.Domain.Exceptions
{
    [Serializable]
    public class SaveChangesFailedException : Exception
    {        
        public SaveChangesFailedException() { }
        public SaveChangesFailedException(string message) : base(message) { }
        public SaveChangesFailedException(string message, Exception inner) : base(message, inner) { }

        public SaveChangesFailedException(string source, string message) : base(message)
        {
            Source = source;
        }
        
        protected SaveChangesFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

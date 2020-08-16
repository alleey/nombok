using System;

namespace Nombok.Shared
{
    public class NombokException : Exception
    {
        public NombokException()
        {
        }

        public NombokException(string message) : base(message)
        {
        }

        public NombokException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

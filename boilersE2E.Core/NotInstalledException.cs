using System.Runtime.Serialization;

namespace boilersE2E.Core
{
    [Serializable]
    public class NotInstalledException : Exception
    {
        public NotInstalledException()
        {
        }

        public NotInstalledException(string? message) : base(message)
        {
        }

        public NotInstalledException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
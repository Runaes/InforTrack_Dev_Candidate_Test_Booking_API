using System;
using System.Runtime.Serialization;

namespace InforTrack_Dev_Candidate_Test_Booking_API.Exceptions
{
    [Serializable]
    internal class BadNameException : Exception
    {
        public BadNameException()
        {
        }

        public BadNameException(string message) : base(message)
        {
        }

        public BadNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadNameException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
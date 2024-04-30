using System;
using System.Runtime.Serialization;

namespace InforTrack_Dev_Candidate_Test_Booking_API.Exceptions
{
    [Serializable]
    internal class BadDataException : Exception
    {
        public BadDataException()
        {
        }

        public BadDataException(string message) : base(message)
        {
        }

        public BadDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadDataException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
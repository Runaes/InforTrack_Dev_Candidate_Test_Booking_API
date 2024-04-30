using System;
using System.Runtime.Serialization;

namespace InforTrack_Dev_Candidate_Test_Booking_API.Exceptions
{
    [Serializable]
    internal class OutOfOfficeHoursException : Exception
    {
        public OutOfOfficeHoursException()
        {
        }

        public OutOfOfficeHoursException(string message) : base(message)
        {
        }

        public OutOfOfficeHoursException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OutOfOfficeHoursException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
using System;

namespace InforTrack_Dev_Candidate_Test_Booking_API
{
    [Serializable]
    internal class TimeConflictException : Exception
    {
        public TimeConflictException()
        {
        }

        public TimeConflictException(string message) : base(message)
        {
        }

        public TimeConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
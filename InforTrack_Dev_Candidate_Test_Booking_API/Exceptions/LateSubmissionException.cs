using System;
using System.Runtime.Serialization;

namespace InforTrack_Dev_Candidate_Test_Booking_API
{
    [Serializable]
    internal class LateSubmissionException : Exception
    {
        public LateSubmissionException()
        {
        }

        public LateSubmissionException(string message) : base(message)
        {
        }

        public LateSubmissionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LateSubmissionException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
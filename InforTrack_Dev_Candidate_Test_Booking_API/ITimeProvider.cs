using System;

namespace InforTrack_Dev_Candidate_Test_Booking_API
{
    public interface ITimeProvider
    {
        DateTime CurrentTime { get; }
    }

    public class BlankTimeProvider : ITimeProvider
    {
        public DateTime CurrentTime => DateTime.MinValue;
    }
}
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InforTrack_Dev_Candidate_Test_Booking_API
{
    public class Booking
    {
        [JsonConstructor]
        public Booking() { }

        [JsonRequired]
        public string BookingTime { get; set; }
        [JsonRequired]
        public string Name { get; set; }

        public Guid BookingId { get; } = Guid.NewGuid();
    }

    public struct BookingID
    {
        public Guid BookingId { get; set; }
    }

}

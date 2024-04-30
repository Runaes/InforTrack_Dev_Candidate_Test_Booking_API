using System;
using System.Collections.Generic;
using System.Linq;

namespace InforTrack_Dev_Candidate_Test_Booking_API
{
    public class Bookings
    {
        public Bookings(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
        }
        ITimeProvider timeProvider;

        List<(DateTime earliest, TimeSpan timeAvailable)> Availabilities = new List<(DateTime, TimeSpan)>
        {
            (DateTime.Today.AddHours(9), TimeSpan.FromHours(8)),
            (DateTime.Today.AddHours(9), TimeSpan.FromHours(8)),
            (DateTime.Today.AddHours(9), TimeSpan.FromHours(8)),
            (DateTime.Today.AddHours(9), TimeSpan.FromHours(8)),
        };

        public IEnumerable<Booking> BookingsList = new List<Booking>();

        bool HasAvailability(DateTime time)
        {
            return Availabilities.Any(i => i.earliest <= time && time.AddHours(1).Subtract(i.earliest) <= i.timeAvailable && i.timeAvailable.TotalHours >= 1);
        }

        public Booking CreateBooking(string timeAsString, string name)
        {

            if (!DateTime.TryParse(timeAsString, out var time))
            {
                throw new BadDataException();
            }
            if (time.Subtract(timeProvider.CurrentTime) < TimeSpan.Zero)
            {
                // special functionality for if they are trying to book a timeslot that has already past - not implemented in release
                throw new LateSubmissionException();
            }
            if (time.Hour < 9 || time.Subtract(DateTime.Today.AddHours(16)) > TimeSpan.Zero)
            {
                throw new OutOfOfficeHoursException();
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new BadNameException();
            }

            if (!HasAvailability(time))
            {
                throw new TimeConflictException();
            }

            var timeslot = Availabilities.First(i => time.Subtract(i.earliest) >= TimeSpan.Zero);
            Availabilities.Remove(timeslot);
            var timespanToEarliest = time.Subtract(timeslot.earliest);

            if (timespanToEarliest > TimeSpan.FromHours(1))
            {
                var newSlot = (timeslot.earliest, timespanToEarliest);
                Availabilities.Add(newSlot);
            }


            timeslot.timeAvailable = timeslot.timeAvailable.Subtract(timespanToEarliest + TimeSpan.FromHours(1));
            timeslot.earliest = time.AddHours(1);
            Availabilities.Add(timeslot);

            var booking = new Booking { BookingTime = timeAsString, Name = name };
            ((List<Booking>)BookingsList).Add(booking);
            return booking;
        }
    }
}

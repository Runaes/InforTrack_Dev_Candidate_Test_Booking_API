using System;
using InforTrack_Dev_Candidate_Test_Booking_API.Exceptions;
using Moq;
using NUnit.Framework;

namespace InforTrack_Dev_Candidate_Test_Booking_API.Testing
{
    [TestFixture]
    public class BookingTest
    {
        [SetUp]
        public void Setup()
        {
            TestTimeProvider = new Mock<ITimeProvider>();
            TestBookings = new Bookings(TestTimeProvider.Object);
            TestTimeProvider.Setup(m => m.CurrentTime).Returns(DateTime.MinValue);
        }
        Mock<ITimeProvider> TestTimeProvider;
        Bookings TestBookings;

        [Test]
        public void Booking_Success_SingleBooking()
        {
            TestBookings.CreateBooking("11:30", "Jason Meyers");
        }

        [Test]
        public void Booking_Success_SingleBooking_LastPossibleTimePeriod()
        {
            TestBookings.CreateBooking("16:00", "Jason Meyers");
        }

        [Test]
        public void Booking_Success_FourBookingsOnSameTimeSlot()
        {
            TestBookings.CreateBooking("11:35", "Jason Meyers");
            TestBookings.CreateBooking("11:35", "Jasper Meyers");
            TestBookings.CreateBooking("11:35", "Jas Meyers");
            TestBookings.CreateBooking("11:35", "Jasmine Meyers");
        }

        [Test]
        public void Booking_Success_FourBookingsOnOverlappingTimeSlots()
        {
            TestBookings.CreateBooking("10:15", "Jason Meyers");
            TestBookings.CreateBooking("11:35", "Jasper Meyers");
            TestBookings.CreateBooking("10:45", "Jas Meyers");
            TestBookings.CreateBooking("11:05", "Jasmine Meyers");
        }

        [Test]
        public void Booking_Success_OneSlotFilled()
        {
            TestBookings.CreateBooking("9:00", "Fill");
            TestBookings.CreateBooking("10:00", "Fill");
            TestBookings.CreateBooking("11:00", "Fill");
            TestBookings.CreateBooking("12:00", "Fill");
            TestBookings.CreateBooking("13:00", "Fill");
            TestBookings.CreateBooking("14:00", "Fill");
            TestBookings.CreateBooking("15:00", "Fill");
            TestBookings.CreateBooking("16:00", "Fill");
        }

        [Test]
        public void Booking_Success_BookingsOnOverlappingTimeSlots_OneSlotFilled()
        {
            TestBookings.CreateBooking("9:00", "Fill");
            TestBookings.CreateBooking("10:00", "Fill");
            TestBookings.CreateBooking("11:00", "Fill");
            TestBookings.CreateBooking("12:00", "Fill");
            TestBookings.CreateBooking("13:00", "Fill");
            TestBookings.CreateBooking("14:00", "Fill");
            TestBookings.CreateBooking("15:00", "Fill");
            TestBookings.CreateBooking("16:00", "Fill");
            TestBookings.CreateBooking("11:35", "Jasper Meyers");
            TestBookings.CreateBooking("10:45", "Jas Meyers");
            TestBookings.CreateBooking("11:05", "Jasmine Meyers");
        }

        [Test]
        public void Booking_Failure_NoNameProvided()
        {
            Assert.Throws<BadNameException>(() => TestBookings.CreateBooking("10:15", null), "A BadNameException should be thrown when name is null");
            Assert.Throws<BadNameException>(() => TestBookings.CreateBooking("10:15", ""), "A BadNameException should be thrown when name is empty");
        }

        [Test]
        public void Booking_Failure_OutsideOfOfficeHours()
        {
            Assert.Throws<OutOfOfficeHoursException>(() => TestBookings.CreateBooking("8:45", "A Name"), "An OutOfOfficeHoursException should be thrown when booking is requested before opening hours");
            Assert.Throws<OutOfOfficeHoursException>(() => TestBookings.CreateBooking("16:01", "A Name"), "An OutOfOfficeHoursException should be thrown when booking would exceed office hours");
            Assert.Throws<OutOfOfficeHoursException>(() => TestBookings.CreateBooking("21:00", "A Name"), "An OutOfOfficeHoursException should be thrown when booking is requested after closing hours");
        }

        [Test]
        public void Booking_Failure_LateSubmission()
        {
            // Test only Case
            TestTimeProvider.Setup(m => m.CurrentTime).Returns(DateTime.Today.AddHours(13));
            Assert.Throws<LateSubmissionException>(() => TestBookings.CreateBooking("10:15", "Jabber"), "Should throw if current time is after request time");
        }

        [Test]
        public void Booking_Failure_Conflict()
        {
            TestBookings.CreateBooking("10:15", "Jason Meyers");
            TestBookings.CreateBooking("11:35", "Jasper Meyers");
            TestBookings.CreateBooking("10:45", "Jas Meyers");
            TestBookings.CreateBooking("11:05", "Jasmine Meyers");
            Assert.Throws<TimeConflictException>(() => TestBookings.CreateBooking("10:55", "Jaspen Meyers"));
        }
    }
}

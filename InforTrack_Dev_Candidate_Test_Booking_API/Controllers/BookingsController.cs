using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InforTrack_Dev_Candidate_Test_Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        static Bookings bookings = new Bookings(new BlankTimeProvider());

        [HttpGet]
        public IEnumerable<Booking> GetBookings()
        {
            return bookings.BookingsList;
        }

        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(string id)
        {
            try
            {
                return bookings.BookingsList.First(booking => booking.BookingId == Guid.Parse(id));
            }
            catch
            {
                return BadRequest("Invalid ID");
            }
        }

        [HttpPost]
        public ActionResult<BookingID> PostRequestBooking(Booking bookingRequest)
        {
            try
            {
                var booking = bookings.CreateBooking(bookingRequest.BookingTime, bookingRequest.Name);
                return new BookingID { BookingId = booking.BookingId};
            }
            catch (BadDataException)
            {
                return BadRequest("Invalid Data");
            }
            catch (BadNameException)
            {
                return BadRequest("Invalid Name");
            }
            catch (OutOfOfficeHoursException)
            {
                return BadRequest("Booking request time falls outside of Office Hours. Office Hours are 9:00 to 17:00. Each appointment takes one Hour.");
            }
            catch (TimeConflictException)
            {
                return Conflict("Your requested booking time is not available.");
            }
        }
    }
}

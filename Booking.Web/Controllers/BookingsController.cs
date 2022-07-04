using Booking.Data.Entities;
using Booking.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService service;
        public BookingsController (IBookingService service)
        {
            this.service = service;

        }

        [HttpPost]
        public IActionResult Create(BookingModel model)
        {
            RoomBooking booking = new RoomBooking
            {
                RoomId = model.RoomId,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                VisitorsCount = model.VisitorsCount,
            };
            this.service.CreateBooking(booking);
            return Ok();
        }
    }
}

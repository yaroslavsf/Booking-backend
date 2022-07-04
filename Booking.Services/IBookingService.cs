using Booking.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services
{
    public interface IBookingService
    {
        public void CreateBooking(RoomBooking booking);
    }

}

using Booking.Data;
using Booking.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Services
{
    public  class RoomService : IRoomService
    {
        private readonly BookingDbContext context;
        public RoomService(BookingDbContext context)
        {
            this.context = context;
        }

        public List<Room> GetAll()
        {
            return context.Rooms.ToList();
        }
       
    }
}

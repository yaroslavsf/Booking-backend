using Booking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService roomService;
        public RoomsController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rooms = roomService.GetAll();
            return Ok(roomService.GetAll());
        }
    }
}

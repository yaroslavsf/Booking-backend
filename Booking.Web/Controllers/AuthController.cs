using Booking.Data.Entities;
using Booking.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;
        public AuthController(IAuthService service)
        {
            this.service = service;
        }

       

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = service.Login(model.Login, model.Password);
                return Ok(new{token});
            }
            catch(Exception ex)
            {
                return Unauthorized();
            }

        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Age = model.Age,
                    Created = DateTime.Now,

                };
                var token = service.Register(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;

namespace Booking.ApiTests
{
    //abstract class with a client just to make inherited test classes less bigger
    public abstract class BaseApiTest
    {
        protected readonly HttpClient _client;
        public BaseApiTest()
        {
            var app = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        //if i want to change some dependencies for testing such as database or some service class
                    });
                });
            _client = app.CreateClient();
        }
    }
}
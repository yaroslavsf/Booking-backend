using System.Net.Http;
using System.Text;
using Xunit;

namespace Booking.ApiTests
{
    public class AuthApiTest : BaseApiTest
    {
        [Fact]
        public async void Registration_Token_Valid()
        {
            var newUserJson = "";
            var encoding = Encoding.UTF8;
            var contentType = "applicatoin/json";
            var response = _client.PostAsync("api/auth", new StringContent(newUserJson, encoding, contentType));

          /*  Assert.True(response.IsSuccessStatusCode);

            var token = await response.Content.ReadAsStringAsync();*/
        }
    }
}

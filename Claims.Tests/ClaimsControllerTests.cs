using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Claims.Tests
{
    public class ClaimsControllerTests
    {
        [Fact]
        public async Task Get_Claims()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(_ =>
                {});

            var client = application.CreateClient();

            var response = await client.GetAsync("/Claims");

            response.EnsureSuccessStatusCode();

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }
    }  
}

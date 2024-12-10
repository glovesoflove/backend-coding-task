using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Claims.Tests
{
    public class CoversControllerTests
    {

        [Fact]
        public async Task Get_Covers()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(_ =>
                {});

            var client = application.CreateClient();

            var response = await client.GetAsync("/Covers");

            response.EnsureSuccessStatusCode();

        }

[Fact]
public async Task CreateCover_ReturnsCreatedCover()
{

    var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(_ => { });

    var client = application.CreateClient();

    var cover = new Cover
    {
      Id = "12345",
      StartDate = DateTime.Parse("2024-12-15"),
      EndDate = DateTime.Parse("2024-12-31"),
      Type = 0,
      Premium = 1000
    };

    var content = new StringContent(JsonConvert.SerializeObject(cover), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));

    var response = await client.PostAsync("/Covers", content);

    response.EnsureSuccessStatusCode();
     var responseContent = await response.Content.ReadAsStringAsync();
     var createdCover = JsonConvert.DeserializeObject<Cover>(responseContent);
     Assert.NotNull(createdCover);
     Assert.Equal(cover.Id, createdCover?.Id);
     Assert.Equal(cover.StartDate, createdCover?.StartDate);
     Assert.Equal(cover.EndDate, createdCover?.EndDate);
     Assert.Equal(cover.Type, createdCover?.Type);
     Assert.Equal(cover.Premium, createdCover?.Premium);
}
    }
}

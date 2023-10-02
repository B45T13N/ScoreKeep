namespace ScorekeepTests.Business.Services;
public class VolunteerServiceShould
{
    [Fact]
    public async Task AddVolunteerAsync_ValidResponse_ReturnsTrue()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com")
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var volunteerService = new VolunteerService(httpClientMock.Object);
        var volunteer = new Volunteer
        {
            GameId = 1,
            Name = "Test",
            VolunteerTypeId = 1,
        };

        // Act
        bool result = await volunteerService.AddVolunteerAsync(volunteer, "1234");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddVolunteerAsync_FailedResponse_ReturnsFalse()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest // Simulate a failed response
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com")
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var volunteerService = new VolunteerService(httpClientMock.Object);
        var volunteer = new Volunteer
        {
            GameId = 1,
            Name = "Test Room Manager"
        };

        // Act
        bool result = await volunteerService.AddVolunteerAsync(volunteer, "1234");

        // Assert
        result.Should().BeFalse();
    }
}


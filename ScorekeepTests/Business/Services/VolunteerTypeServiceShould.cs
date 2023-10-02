namespace ScorekeepTests.Business.Services;
public class VolunteerTypeserviceShould
{
    [Fact]
    public async Task GetAllVolunteerTypesAsync_Success()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();

        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"data\": [ { \"id\": 1, \"label\": \"Type 1\"}, {\"id\": \"2\", \"label\": \"Type 2\" } ] }")
        });


        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com")
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var volunteerTypeService = new VolunteerTypeService(httpClientMock.Object);

        // Act
        List<VolunteerType> volunteerTypes = await volunteerTypeService.GetAllVolunteerTypesAsync();

        // Assert
        volunteerTypes.Should().HaveCount(2);
        volunteerTypes[0].Id.Should().Be(1);
        volunteerTypes[0].Label.Should().Be("Type 1");
        volunteerTypes[1].Id.Should().Be(2);
        volunteerTypes[1].Label.Should().Be("Type 2");
    }

    [Fact]
    public async Task GetAllVolunteerTypesAsync_Error()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();

        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"data\": [] }")
        });


        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com")
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var volunteerTypeService = new VolunteerTypeService(httpClientMock.Object);

        // Act
        Func<Task> act = async () => await volunteerTypeService.GetAllVolunteerTypesAsync();

        // Assert (Exception is expected)
        await act.Should().ThrowAsync<VolunteerTypeNotFound>();
    }
}


namespace ScorekeepTests.Business.Services;
public class LocalTeamServiceShould
{
    [Fact]
    public async Task GetLocalTeamsAsync_ValidResponse_ReturnsListOfLocalTeams()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"data\": [ { \"id\": 1, \"name\": \"Test Name\", \"logo\": \"Test logo\" } ] }")
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri(API.URI)
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var localTeamService = new LocalTeamService(httpClientMock.Object);

        // Act
        List<LocalTeam> localTeams = await localTeamService.GetLocalTeamsAsync();

        // Assert
        localTeams.Should().HaveCount(1);
        localTeams[0].Id.Should().Be(1);
        localTeams[0].Logo.Should().Be("Test logo");
        localTeams[0].Name.Should().Be("Test Name");
    }

    [Fact]
    public async Task GetLocalTeamsAsyncNoGamesThrowsLocalTeamNotFoundException()
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
            BaseAddress = new Uri("https://example.com/")
        };

        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var localTeamService = new LocalTeamService(httpClientMock.Object);

        // Act
        Func<Task> act = async () => await localTeamService.GetLocalTeamsAsync();

        // Assert (Exception is expected)
        await act.Should().ThrowAsync<LocalTeamNotFoundException>();
    }

    [Fact]
    public async Task GetLocalTeamAsync_ValidResponse_ReturnsLocalTeam()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"data\": { \"id\": 1, \"name\": \"Test Name\", \"logo\": \"Test logo\" } }")
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com/")
        };

        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var localTeamService = new LocalTeamService(httpClientMock.Object);

        // Act
        LocalTeam localTeam = await localTeamService.GetLocalTeamAsync(1);

        // Assert
        localTeam.Id.Should().Be(1);
        localTeam.Logo.Should().Be("Test logo");
        localTeam.Name.Should().Be("Test Name");
    }

    [Fact]
    public async Task GetLocalTeamAsyncFailedResponseThrowsConnexionExceptionAsync()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();

        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com/")
        };

        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var localTeamService = new LocalTeamService(httpClientMock.Object);

        // Act
        Func<Task> act = async () => await localTeamService.GetLocalTeamAsync(1);

        // Assert (Exception is expected)
        await act.Should().ThrowAsync<ConnexionException>();
    }
}


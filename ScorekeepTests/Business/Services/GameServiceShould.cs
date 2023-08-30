namespace ScorekeepTests.Business.Services;
public class GameServiceShould
{
    [Fact]
    public async Task GetGamesAsync_ValidResponse_ReturnsListOfGames()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"data\": [ { \"id\": 1, \"address\": \"Test Address\", \"category\": \"Test Category\", \"gameDate\": \"2023-08-30T12:00:00Z\" } ] }")
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri(API.URI)
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var gameService = new GameService(httpClientMock.Object);

        // Act
        List<Game> games = await gameService.GetGamesAsync(1);

        // Assert
        games.Should().HaveCount(1);
        games[0].Id.Should().Be(1);
        games[0].Address.Should().Be("Test Address");
        games[0].Category.Should().Be("Test Category");
        games[0].GameDate.Should().Be(new DateTime(2023, 8, 30, 12, 0, 0, DateTimeKind.Utc));
    }

    [Fact]
    public async Task GetGamesAsyncNoGamesThrowsGameNotFoundException()
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

        var gameService = new GameService(httpClientMock.Object);

        // Act
        Func<Task> act = async () => await gameService.GetGamesAsync(1);

        // Assert (Exception is expected)
        await act.Should().ThrowAsync<GameNotFoundException>();
    }

    [Fact]
    public async Task GetGameAsync_ValidResponse_ReturnsGame()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{ \"data\": { \"id\": 1, \"address\": \"Test Address\", \"category\": \"Test Category\", \"gameDate\": \"2023-08-30T12:00:00Z\" } }")
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com/")
        };

        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var gameService = new GameService(httpClientMock.Object);

        // Act
        Game game = await gameService.GetGameAsync(1);

        // Assert
        game.Id.Should().Be(1);
        game.Address.Should().Be("Test Address");
        game.Category.Should().Be("Test Category");
        game.GameDate.Should().Be(new DateTime(2023, 8, 30, 12, 0, 0, DateTimeKind.Utc));
    }

    [Fact]
    public async Task GetGameAsyncFailedResponseThrowsConnexionException()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();

        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound // Simulate a failed response
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com/")
        };

        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var gameService = new GameService(httpClientMock.Object);

        // Act
        Func<Task> act = async () => await gameService.GetGameAsync(1);

        // Assert (Exception is expected)
        await act.Should().ThrowAsync<ConnexionException>();
    }

}


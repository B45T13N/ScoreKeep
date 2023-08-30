namespace ScorekeepTests.Business.Services;
public class RoomManagerServiceShould
{
    [Fact]
    public async Task AddRoomManagerAsync_ValidResponse_ReturnsTrue()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com") // Set a valid base address
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var roomManagerService = new RoomManagerService(httpClientMock.Object);
        var roomManager = new RoomManager
        {
            Email = "test@example.com",
            GameId = 1,
            Name = "Test Room Manager"
        };

        // Act
        bool result = await roomManagerService.AddRoomManagerAsync(roomManager);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddRoomManagerAsync_FailedResponse_ReturnsFalse()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest // Simulate a failed response
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com") // Set a valid base address
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var roomManagerService = new RoomManagerService(httpClientMock.Object);
        var roomManager = new RoomManager
        {
            Email = "test@example.com",
            GameId = 1,
            Name = "Test Room Manager"
        };

        // Act
        bool result = await roomManagerService.AddRoomManagerAsync(roomManager);

        // Assert
        result.Should().BeFalse();
    }
    [Fact]
    public async Task UpdateRoomManagerAsync_ValidResponse_ReturnsTrue()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com") // Set a valid base address
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var roomManagerService = new RoomManagerService(httpClientMock.Object);
        var roomManager = new RoomManager
        {
            Email = "test@example.com",
            GameId = 1,
            Name = "Test Room Manager"
        };

        // Act
        bool result = await roomManagerService.UpdateRoomManagerAsync(roomManager);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateRoomManagerAsync_FailedResponse_ReturnsFalse()
    {
        // Arrange
        var httpClientMock = new Mock<IHttpClientProvider>();
        var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest // Simulate a failed response
        });

        var httpClient = new HttpClient(fakeHttpMessageHandler)
        {
            BaseAddress = new Uri("https://example.com") // Set a valid base address
        };
        httpClientMock.Setup(provider => provider.CreateHttpClient()).Returns(httpClient);

        var roomManagerService = new RoomManagerService(httpClientMock.Object);
        var roomManager = new RoomManager
        {
            Email = "test@example.com",
            GameId = 1,
            Name = "Test Room Manager"
        };

        // Act
        bool result = await roomManagerService.UpdateRoomManagerAsync(roomManager);

        // Assert
        result.Should().BeFalse();
    }

}


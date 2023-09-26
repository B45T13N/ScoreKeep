namespace ScorekeepTests.Business.Services;
public class SecretaryServiceShould
{
    [Fact]
    public async Task AddSecretaryAsync_ValidResponse_ReturnsTrue()
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

        var secretaryService = new SecretaryService(httpClientMock.Object);
        var roomManager = new Secretary
        {
            Email = "test@example.com",
            GameId = 1,
            Name = "Test Room Manager"
        };

        // Act
        bool result = await secretaryService.AddSecretaryAsync(roomManager, "1234");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddSecretaryAsync_FailedResponse_ReturnsFalse()
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

        var secretaryService = new SecretaryService(httpClientMock.Object);
        var roomManager = new Secretary
        {
            Email = "test@example.com",
            GameId = 1,
            Name = "Test Room Manager"
        };

        // Act
        bool result = await secretaryService.AddSecretaryAsync(roomManager, "1234");

        // Assert
        result.Should().BeFalse();
    }
    [Fact]
    public async Task UpdateSecretaryAsync_ValidResponse_ReturnsTrue()
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

        var secretaryService = new SecretaryService(httpClientMock.Object);
        var roomManager = new Secretary
        {
            Email = "test@example.com",
            GameId = 1,
            Name = "Test Room Manager"
        };

        // Act
        bool result = await secretaryService.UpdateSecretaryAsync(roomManager);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateSecretaryAsync_FailedResponse_ReturnsFalse()
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

        var secretaryService = new SecretaryService(httpClientMock.Object);
        var roomManager = new Secretary
        {
            Email = "test@example.com",
            GameId = 1,
            Name = "Test Room Manager"
        };

        // Act
        bool result = await secretaryService.UpdateSecretaryAsync(roomManager);

        // Assert
        result.Should().BeFalse();
    }

}


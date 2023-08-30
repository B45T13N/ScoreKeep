namespace ScorekeepTests.Business.Providers;
public class HttpClientProviderShould
{
    [Fact]
    public void HttpClientProviderShouldReturnHttpClientOnCreate()
    {
        // Arrange
        var httpClientProvider = new HttpClientProvider();

        // Act
        var httpClient = httpClientProvider.CreateHttpClient();

        // Assert
        Assert.NotNull(httpClient);
        Assert.IsType<HttpClient>(httpClient);
    }
}


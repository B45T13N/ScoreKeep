using System.Net.Http.Headers;

namespace ScoreKeep.Business.Providers;

public class HttpClientProvider
{

    public HttpClient CreateHttpClient()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(API.URI);
        httpClient.DefaultRequestHeaders.Add("Scorekeep-API-Key", API.APIKey);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return httpClient;
    }
}

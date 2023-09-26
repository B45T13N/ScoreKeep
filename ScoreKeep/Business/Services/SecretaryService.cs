using Newtonsoft.Json;
using System.Text;

namespace ScoreKeep.Business.Services;

public class SecretaryService : ISecretaryService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl = "/api/secretaries";

    public SecretaryService(IHttpClientProvider httpClientProvider)
    {
        _httpClient = httpClientProvider.CreateHttpClient();
    }

    public async Task<bool> AddSecretaryAsync(Secretary secretary, String password)
    {
        try
        {
            var data = new Dictionary<string, string>
            {
                { "token", password},
                { "gameId", secretary.GameId.ToString() },
                { "name", secretary.Name }
            };

            var jsonData = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{ApiUrl}/store", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            // Gérer l'erreur de la connexion Internet
            throw new Exception("Failed to connect to the API.", ex);
        }
    }

    public async Task<bool> UpdateSecretaryAsync(Secretary secretary)
    {
        try
        {
            var data = new Dictionary<string, string>
            {
                { "email", secretary.Email },
                { "gameId", secretary.GameId.ToString() },
                { "name", secretary.Name }
            };

            var jsonData = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{ApiUrl}/update", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            // Gérer l'erreur de la connexion Internet
            throw new Exception("Failed to connect to the API.", ex);
        }
    }
}
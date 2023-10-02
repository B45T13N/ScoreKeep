using Newtonsoft.Json;
using System.Text;

namespace ScoreKeep.Business.Services;

public class VolunteerService : IVolunteerService
{
    private readonly HttpClient _httpClient;

    public const string ApiUrl = "/api/volunteers";

    public VolunteerService(IHttpClientProvider httpClientProvider)
    {
        _httpClient = httpClientProvider.CreateHttpClient();

    }

    public async Task<bool> AddVolunteerAsync(Volunteer volunteer, String password)
    {
        try
        {
            var data = new Dictionary<string, string>
            {
                { "token", password},
                { "gameId", volunteer.GameId.ToString() },
                { "name", volunteer.Name },
                { "volunteerTypeId", volunteer.VolunteerTypeId.ToString() }
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
}
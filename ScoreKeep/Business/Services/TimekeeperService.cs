using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ScoreKeep.Business.Services;

public class TimekeeperService : ITimekeeperService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl = "/api/timekeepers";

    public TimekeeperService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api-score-keep.bds-dev.fr");
        _httpClient.DefaultRequestHeaders.Add("Scorekeep-API-Key", "mfevFIuk3TBCq619LTttzEd0joOkO1YxRPC5G4RQ8tY=");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<bool> AddTimekeeperAsync(Timekeeper timekeeper)
    {
        try
        {
            var data = new Dictionary<string, string>
            {
                { "email", timekeeper.Email },
                { "gameId", timekeeper.GameId.ToString() },
                { "name", timekeeper.Name }
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

    public async Task<bool> UpdateTimekeeperAsync(Timekeeper timekeeper)
    {
        try
        {
            var data = new Dictionary<string, string>
            {
                { "email", timekeeper.Email },
                { "gameId", timekeeper.GameId.ToString() },
                { "name", timekeeper.Name }
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
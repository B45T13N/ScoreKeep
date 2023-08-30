using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ScoreKeep.Business.Services;

public class LocalTeamService : ILocalTeamService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl = "/api/local-teams";

    public LocalTeamService(IHttpClientProvider httpClientProvider)
    {
        _httpClient = httpClientProvider.CreateHttpClient();
    }

    public async Task<List<LocalTeam>> GetLocalTeamsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(ApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                List<LocalTeam> localTeams = DeserializeLocalTeams(responseBody);

                if (localTeams.Count == 0)
                    throw new LocalTeamNotFoundException();

                return localTeams;
            }
            else
            {
                // Gérer l'erreur de la requête
                throw new ConnexionException($"Failed to retrieve games. StatusCode: {response.StatusCode}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<LocalTeam> GetLocalTeamAsync(int localTeamId)
    {
        var response = await _httpClient.GetAsync($"{ApiUrl}/{localTeamId}");

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            LocalTeam localTeam = DeserializeLocalTeam(responseBody);

            return localTeam;
        }
        else
        {
            // Gérer l'erreur de la requête
            throw new ConnexionException($"Failed to retrieve game with Id: {localTeamId}. StatusCode: {response.StatusCode}");
        }
    }

    private LocalTeam DeserializeLocalTeam(string responseBody)
    {
        var localTeamData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);
        var localTeamObj = (JObject)localTeamData["data"];

        var localTeam = new LocalTeam
        {
            Id = Convert.ToInt32(localTeamObj["id"]),
            Logo = Convert.ToString(localTeamObj["logo"]),
            Name = Convert.ToString(localTeamObj["name"]),
        };

        return localTeam;
    }

    private List<LocalTeam> DeserializeLocalTeams(string responseBody)
    {
        var localTeamData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);
        var localTeamListObj = (JArray)localTeamData["data"];

        var localTeams = new List<LocalTeam>();

        foreach (var localTeamObj in localTeamListObj)
        {
            var tmpLocalTeamObj = (JObject)localTeamObj;

            var localTeam = new LocalTeam
            {
                Id = Convert.ToInt32(tmpLocalTeamObj["id"]),
                Logo = Convert.ToString(tmpLocalTeamObj["logo"]),
                Name = Convert.ToString(tmpLocalTeamObj["name"]),
            };

            localTeams.Add(localTeam);
        }

        return localTeams;
    }
}
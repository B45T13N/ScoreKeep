using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ScoreKeep.Business.Services;

public class VolunteerTypeService : IVolunteerTypeService
{
    private readonly HttpClient _httpClient;

    public const string ApiUrl = "/api/volunteer-types";

    public VolunteerTypeService(IHttpClientProvider httpClientProvider)
    {
        _httpClient = httpClientProvider.CreateHttpClient();

    }

    public async Task<List<VolunteerType>> GetAllVolunteerTypesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/show-all");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                List<VolunteerType> volunteerTypes = DeserializeVolunteerTypes(responseBody);

                if (volunteerTypes.Count == 0)
                    throw new VolunteerTypeNotFound();

                return volunteerTypes;
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

    private List<VolunteerType> DeserializeVolunteerTypes(string json)
    {
        var volunteerTypeData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        var volunteerTypesData = (JArray)volunteerTypeData["data"];

        var volunteerTypes = new List<VolunteerType>();

        foreach (var volunteerTypeObj in volunteerTypesData)
        {
            var volunteerTypeDataObj = (JObject)volunteerTypeObj;

            var volunteerType = new VolunteerType
            {
                Id = Convert.ToInt32(volunteerTypeDataObj["id"]),
                Label = Convert.ToString(volunteerTypeDataObj["label"]),
            };

            volunteerTypes.Add(volunteerType);
        }

        return volunteerTypes;
    }
}
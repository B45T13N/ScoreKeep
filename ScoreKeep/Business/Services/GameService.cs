using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ScoreKeep.Business.Services;

public class GameService : IGameService
{
    private readonly HttpClient _httpClient;

    private const string ApiUrl = "/api/games";

    public GameService(HttpClientProvider httpClientProvider)
    {
        _httpClient = httpClientProvider.CreateHttpClient();
    }

    public async Task<List<Game>> GetGamesAsync(int localTeamId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}?local_team_id={localTeamId}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                List<Game> games = DeserializeGames(responseBody);

                return games;
            }
            else
            {
                // Gérer l'erreur de la requête
                throw new Exception($"Failed to retrieve games. StatusCode: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Gérer l'erreur de la connexion Internet
            throw new Exception("Failed to connect to the API.", ex);
        }
    }

    public async Task<Game> GetGameAsync(int gameId)
    {
        var response = await _httpClient.GetAsync($"{ApiUrl}/{gameId}");

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            Game game = DeserializeGame(responseBody);

            return game;
        }
        else
        {
            // Gérer l'erreur de la requête
            throw new Exception($"Failed to retrieve game with Id: {gameId}. StatusCode: {response.StatusCode}");
        }
    }

    public async Task<bool> UpdateGameAsync(int gameId, string fieldName, int fieldId)
    {
        try
        {
            var data = new Dictionary<string, string>
            {
                { fieldName, fieldId.ToString() }
            };

            var jsonData = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{ApiUrl}/{gameId}", content);

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
            // Gérer les éventuelles exceptions
            Console.WriteLine($"Erreur lors de la mise à jour du jeu : {ex.Message}");
            return false;
        }
    }

    private Game DeserializeGame(string json)
    {
        var gameData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        var gameDataObj = (JObject)gameData["data"];

        var game = new Game
        {
            Id = Convert.ToInt32(gameDataObj["id"]),
            Address = Convert.ToString(gameDataObj["address"]),
            Category = Convert.ToString(gameDataObj["category"]),
            GameDate = Convert.ToDateTime(gameDataObj["gameDate"])
        };

        if (gameDataObj.ContainsKey("visitorTeam") && gameDataObj["visitorTeam"] != null)
        {
            var visitorTeamData = (JObject)gameDataObj["visitorTeam"];
            game.VisitorTeam = new VisitorTeam
            {
                Id = Convert.ToInt32(visitorTeamData["id"]),
                Name = Convert.ToString(visitorTeamData["name"])
            };
        }

        if (gameDataObj.ContainsKey("localTeam") && gameDataObj["localTeam"] != null)
        {
            var localTeamData = (JObject)gameDataObj["localTeam"];
            game.LocalTeam = new LocalTeam
            {
                Id = Convert.ToInt32(localTeamData["id"]),
                Name = Convert.ToString(localTeamData["name"]),
                Logo = Convert.ToString(localTeamData["logo"])
            };
        }

        if (gameDataObj.ContainsKey("timekeeper") && gameDataObj["timekeeper"] is JObject)
        {
            var timekeeperData = (JObject)gameDataObj["timekeeper"];
            game.Timekeeper = new Timekeeper
            {
                Id = Convert.ToInt32(timekeeperData["id"]),
                Name = Convert.ToString(timekeeperData["name"]),
                Email = Convert.ToString(timekeeperData["email"])
            };
        }

        if (gameDataObj.ContainsKey("secretary") && gameDataObj["secretary"] is JObject)
        {
            var secretaryData = (JObject)gameDataObj["secretary"];
            game.Secretary = new Secretary
            {
                Id = Convert.ToInt32(secretaryData["id"]),
                Name = Convert.ToString(secretaryData["name"]),
                Email = Convert.ToString(secretaryData["email"])
            };
        }

        if (gameDataObj.ContainsKey("roomManager") && gameDataObj["roomManager"] is JObject)
        {
            var roomManagerData = (JObject)gameDataObj["roomManager"];
            game.RoomManager = new RoomManager
            {
                Id = Convert.ToInt32(roomManagerData["id"]),
                Name = Convert.ToString(roomManagerData["name"]),
                Email = Convert.ToString(roomManagerData["email"])
            };
        }

        return game;
    }


    private List<Game> DeserializeGames(string json)
    {
        var gameData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        var gamesData = (JArray)gameData["data"];

        var games = new List<Game>();

        foreach (var gameObj in gamesData)
        {
            var gameDataObj = (JObject)gameObj;

            var game = new Game
            {
                Id = Convert.ToInt32(gameDataObj["id"]),
                Address = Convert.ToString(gameDataObj["address"]),
                Category = Convert.ToString(gameDataObj["category"]),
                GameDate = Convert.ToDateTime(gameDataObj["gameDate"])
            };

            if (gameDataObj.ContainsKey("visitorTeam") && gameDataObj["visitorTeam"] != null)
            {
                var visitorTeamData = (JObject)gameDataObj["visitorTeam"];
                game.VisitorTeam = new VisitorTeam
                {
                    Id = Convert.ToInt32(visitorTeamData["id"]),
                    Name = Convert.ToString(visitorTeamData["name"])
                };
            }

            if (gameDataObj.ContainsKey("localTeam") && gameDataObj["localTeam"] != null)
            {
                var localTeamData = (JObject)gameDataObj["localTeam"];
                game.LocalTeam = new LocalTeam
                {
                    Id = Convert.ToInt32(localTeamData["id"]),
                    Name = Convert.ToString(localTeamData["name"]),
                    Logo = Convert.ToString(localTeamData["logo"])
                };
            }

            if (gameDataObj.ContainsKey("timekeeper") && gameDataObj["timekeeper"] is JObject)
            {
                var timekeeperData = (JObject)gameDataObj["timekeeper"];
                game.Timekeeper = new Timekeeper
                {
                    Id = Convert.ToInt32(timekeeperData["id"]),
                    Name = Convert.ToString(timekeeperData["name"]),
                    Email = Convert.ToString(timekeeperData["email"])
                };
            }

            if (gameDataObj.ContainsKey("secretary") && gameDataObj["secretary"] is JObject)
            {
                var secretaryData = (JObject)gameDataObj["secretary"];
                game.Secretary = new Secretary
                {
                    Id = Convert.ToInt32(secretaryData["id"]),
                    Name = Convert.ToString(secretaryData["name"]),
                    Email = Convert.ToString(secretaryData["email"])
                };
            }

            if (gameDataObj.ContainsKey("roomManager") && gameDataObj["roomManager"] is JObject)
            {
                var roomManagerData = (JObject)gameDataObj["roomManager"];
                game.RoomManager = new RoomManager
                {
                    Id = Convert.ToInt32(roomManagerData["id"]),
                    Name = Convert.ToString(roomManagerData["name"]),
                    Email = Convert.ToString(roomManagerData["email"])
                };
            }

            games.Add(game);
        }

        return games;
    }

}


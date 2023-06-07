using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ScoreKeep.Business.Services;

public class GameService : IGameService
{
    private readonly HttpClient _httpClient;

    public GameService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api-score-keep.bds-dev.fr");
        _httpClient.DefaultRequestHeaders.Add("Scorekeep-API-Key", "mfevFIuk3TBCq619LTttzEd0joOkO1YxRPC5G4RQ8tY=");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Game>> GetGamesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/games");

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
        var response = await _httpClient.GetAsync($"/api/games/{gameId}");

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

    public async Task<Game> CreateGameAsync(Game game)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/games", game);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            Game createdGame = DeserializeGame(responseBody);

            return createdGame;
        }
        else
        {
            // Gérer l'erreur de la requête
            throw new Exception($"Failed to create game. StatusCode: {response.StatusCode}");
        }
    }

    public async Task<Game> UpdateGameAsync(int gameId, Game game)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/games/{gameId}", game);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            Game updatedGame = DeserializeGame(responseBody);

            return updatedGame;
        }
        else
        {
            // Gérer l'erreur de la requête
            throw new Exception($"Failed to update game with Id: {gameId}. StatusCode: {response.StatusCode}");
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

        if (gameDataObj.ContainsKey("timekeeper") && gameDataObj["timekeeper"] != null)
        {
            var timekeeperData = (JObject)gameDataObj["timekeeper"];
            game.Timekeeper = new Timekeeper
            {
                Id = Convert.ToInt32(timekeeperData["id"]),
                Name = Convert.ToString(timekeeperData["name"]),
                Email = Convert.ToString(timekeeperData["email"])
            };
        }

        if (gameDataObj.ContainsKey("secretary") && gameDataObj["secretary"] != null)
        {
            var secretaryData = (JObject)gameDataObj["secretary"];
            game.Secretary = new Secretary
            {
                Id = Convert.ToInt32(secretaryData["id"]),
                Name = Convert.ToString(secretaryData["name"]),
                Email = Convert.ToString(secretaryData["email"])
            };
        }

        if (gameDataObj.ContainsKey("room_manager") && gameDataObj["room_manager"] != null)
        {
            var roomManagerData = (JObject)gameDataObj["room_manager"];
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

            if (gameDataObj.ContainsKey("room_manager") && gameDataObj["room_manager"] is JObject)
            {
                var roomManagerData = (JObject)gameDataObj["room_manager"];
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


    private string SerializeGame(Game game)
    {
        // Implémenter la logique pour sérialiser l'objet Game en JSON
        throw new NotImplementedException();
    }

}


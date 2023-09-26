﻿using Newtonsoft.Json;
using System.Text;

namespace ScoreKeep.Business.Services;

public class RoomManagerService : IRoomManagerService
{
    private readonly HttpClient _httpClient;

    public const string ApiUrl = "/api/room-managers";

    public RoomManagerService(IHttpClientProvider httpClientProvider)
    {
        _httpClient = httpClientProvider.CreateHttpClient();

    }

    public async Task<bool> AddRoomManagerAsync(RoomManager roomManager, String password)
    {
        try
        {
            var data = new Dictionary<string, string>
            {
                { "token", password},
                { "gameId", roomManager.GameId.ToString() },
                { "name", roomManager.Name }
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

    public async Task<bool> UpdateRoomManagerAsync(RoomManager roomManager)
    {
        try
        {
            var data = new Dictionary<string, string>
            {
                { "email", roomManager.Email },
                { "gameId", roomManager.GameId.ToString() },
                { "name", roomManager.Name }
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
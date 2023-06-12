namespace ScoreKeep.Business.Interfaces;
public interface IGameService
{
    Task<List<Game>> GetGamesAsync();
    Task<Game> GetGameAsync(int gameId);
    Task<bool> UpdateGameAsync(int gameId, string fieldName, int fieldId);
}


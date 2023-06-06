namespace ScoreKeep.Business.Interfaces;
public interface IGameService
{
    Task<List<Game>> GetGamesAsync();
    Task<Game> GetGameAsync(int gameId);
    Task<Game> CreateGameAsync(Game game);
    Task<Game> UpdateGameAsync(int gameId, Game game);
}


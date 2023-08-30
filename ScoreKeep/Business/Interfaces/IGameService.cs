namespace ScoreKeep.Business.Interfaces;
public interface IGameService
{
    Task<List<Game>> GetGamesAsync(int localTeamId);
    Task<Game> GetGameAsync(int gameId);
}


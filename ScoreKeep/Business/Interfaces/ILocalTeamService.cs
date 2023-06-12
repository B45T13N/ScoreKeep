namespace ScoreKeep.Business.Interfaces;

public interface ILocalTeamService
{
    Task<List<LocalTeam>> GetLocalTeamsAsync();
    Task<LocalTeam> GetLocalTeamAsync(int localTeamId);
}
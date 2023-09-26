namespace ScoreKeep.Business.Interfaces;

public interface ITimekeeperService
{
    Task<bool> AddTimekeeperAsync(Timekeeper timekeeper, String password);
    Task<bool> UpdateTimekeeperAsync(Timekeeper timekeeper);
}
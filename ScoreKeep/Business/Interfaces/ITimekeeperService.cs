namespace ScoreKeep.Business.Interfaces;

public interface ITimekeeperService
{
    Task<bool> AddTimekeeperAsync(Timekeeper timekeeper);
    Task<bool> UpdateTimekeeperAsync(Timekeeper timekeeper);
}
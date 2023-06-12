namespace ScoreKeep.Business.Interfaces;

public interface ISecretaryService
{
    Task<bool> AddSecretaryAsync(Secretary secretary);
    Task<bool> UpdateSecretaryAsync(Secretary secretary);
}
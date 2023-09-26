namespace ScoreKeep.Business.Interfaces;

public interface ISecretaryService
{
    Task<bool> AddSecretaryAsync(Secretary secretary, String password);
    Task<bool> UpdateSecretaryAsync(Secretary secretary);
}
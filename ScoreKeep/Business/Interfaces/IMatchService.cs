using ScoreKeep.Business.Models;

namespace ScoreKeep.Business.Interfaces;

public interface IMatchService
{
    Task<IEnumerable<Match>> GetMatchListAsync(CancellationToken cancellationToken);

    Task<Match> GetMatchByIdAsync(int id, CancellationToken cancellationToken);
}


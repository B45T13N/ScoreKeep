using ScoreKeep.Business.Interfaces;

namespace ScoreKeep.Business.Services;

public class MatchService : IMatchService
{
    public Task<IEnumerable<Match>> GetMatchListAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Match> GetMatchByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


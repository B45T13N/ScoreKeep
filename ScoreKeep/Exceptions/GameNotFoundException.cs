namespace ScoreKeep.Exceptions;
public class GameNotFoundException : Exception
{
    public GameNotFoundException() : base()
    {
    }
    public GameNotFoundException(string message) : base(message)
    {
    }
}


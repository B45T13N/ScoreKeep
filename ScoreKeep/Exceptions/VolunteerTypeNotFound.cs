namespace ScoreKeep.Exceptions;
public class VolunteerTypeNotFound : Exception
{
    public VolunteerTypeNotFound() : base()
    {
    }
    public VolunteerTypeNotFound(string message) : base(message)
    {
    }
}


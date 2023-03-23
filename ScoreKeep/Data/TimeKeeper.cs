using ScoreKeep.Data.Interface;

namespace ScoreKeep.Data;

public class TimeKeeper : Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
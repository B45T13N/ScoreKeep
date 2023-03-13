using ScoreKeep.Models.Interface;

namespace ScoreKeep.Models;

public class RoomManager : Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
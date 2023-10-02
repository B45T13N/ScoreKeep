namespace ScoreKeep.Business.Models;

public class Game
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public LocalTeam LocalTeam { get; set; }
    public DateTime GameDate { get; set; }
    public Volunteer Timekeeper { get; set; }
    public Volunteer Secretary { get; set; }
    public Volunteer RoomManager { get; set; }
    public VisitorTeam VisitorTeam { get; set; }
    public bool IsHomeMatch { get; set; }
}
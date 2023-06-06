namespace ScoreKeep.Business.Models;

public class Game
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public DateTime GameDate { get; set; }
    public Timekeeper Timekeeper { get; set; }
    public Secretary Secretary { get; set; }
    public RoomManager RoomManager { get; set; }
    public VisitorTeam VisitorTeam { get; set; }
}
namespace ScoreKeep.Business.Models;
public class Volunteer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int GameId { get; set; }
    public int VolunteerTypeId { get; set; }
}
